<?php
class colorArray extends BasicArrayTable {
	public function __construct(db $db){
		$this->db = $db;
		$this->pdo = $db->pdo;

		$this->insertColorSymbol = $this->pdo->prepare("INSERT INTO Color (symbol) VALUES (:colorSymbol)");
		$this->insertColorSymbol->bindParam(":colorSymbol",$this->colorSymbol);

		$this->insertColorCombination = $this->pdo->prepare("INSERT INTO ColorCombinations () VALUES ()");

		$this->insertColorInCombinations = $this->pdo->prepare("INSERT INTO ColorsInCombinations (colorId,combinationid) VALUES (:colorId,:combinationid)");
		$this->insertColorInCombinations->bindParam(":colorId", $this->colorId);
		$this->insertColorInCombinations->bindParam(":combinationid",$this->combinationId);

		$this->symbolToId = $this->pdo->prepare("SELECT id FROM Color WHERE symbol= :colorSymbol");
		$this->symbolToId->bindParam(":colorSymbol",$this->colorSymbol);

		$this->getColorsInColorCombination = $this->pdo->prepare("SELECT colorId AS id FROM ColorsInCombinations WHERE combinationid = :combinationid");
		$this->getColorsInColorCombination->bindParam(":combinationid",$this->combinationId);
		$this->getColorArraysBySingleColor = $this->pdo->prepare("
			SELECT ColorCombinations.id
			FROM ColorCombinations,ColorsInCombinations
			WHERE ColorsInCombinations.colorId=:colorId
			AND ColorsInCombinations.combinationId=ColorCombinations.id
		");
		$this->getColorArraysBySingleColor->bindParam(":colorId",$this->colorId);
		$this->cache = array();
	}
	public function getPossibleArrays(array $colorArray){
		if(count($colorArray)===0){
			$str = '
				SELECT id FROM ColorCombinations WHERE ColorCombinations.id NOT IN(
					SELECT ColorsInCombinations.combinationid
					FROM ColorsInCombinations
				)
			';
		} else {
			$str = '
				SELECT id FROM ColorCombinations WHERE ColorCombinations.id IN (
					SELECT ColorsInCombinations.combinationid
					FROM ColorsInCombinations
					WHERE ColorsInCombinations.colorId IN({ARRAY})
				)
			';
		}

		return parent::runSQL($str,$colorArray);
	}
	public function insertArray(array $colorArray){
		return parent::runInsertQueries($colorArray);
	}
	public function arrayInsert(){
		$this->db->saveExec($this->insertColorCombination);
	}
	public function setArrayHolder($id){
		$this->combinationId = $id;
	}
	public function elementInsert($element){
		$this->colorId = $element;
		$this->db->saveExec($this->insertColorInCombinations);
	}
	public function getAllElementsInArray(int $colorArrayId){
		$this->combinationId = $colorArrayId;
		$this->db->saveExec($this->getColorsInColorCombination);
		return $this->getColorsInColorCombination->fetchAll(PDO::FETCH_ASSOC);
	}
	public function getColorId(string $colorSymbol){
		$this->colorSymbol = $colorSymbol;
		$this->db->saveExec($this->symbolToId);
		$id = $this->symbolToId->fetch(PDO::FETCH_ASSOC)["id"] ?? false;
		if(!$id){
			$this->db->saveExec($this->insertColorSymbol);
			return $this->pdo->lastInsertId();
		}
		return $id;
	}
	public function getColorArrayContainingColor(int $colorId){
		$this->colorId = $colorId;
		$this->db->saveExec($this->getColorArraysBySingleColor);
		$data = array_map(function($v){return $v["id"];},$this->getColorArraysBySingleColor->fetchAll(PDO::FETCH_ASSOC));
		return $data;
	}
	public function somethingElse(array $colorArrayIds,$colorId){
		$query = "
			SELECT ColorCombinations.id
			FROM ColorCombinations
			INNER JOIN ColorsInCombinations
			ON ColorsInCombinations.combinationId=ColorCombinations.id
			WHERE ColorCombinations.id IN ({array})
			AND ColorsInCombinations.colorId = $colorId
		";
		$lineIdsAsString = implode($colorArrayIds,",");
		$queryReplaced = str_replace("{array}",$lineIdsAsString,$query);
		$res = $this->pdo->query($queryReplaced);
		if(!$res){
			echo $queryReplaced;
		}
		return array_map(function($v){return $v["id"];},$res->fetchAll(PDO::FETCH_ASSOC));
	}
	public function makeArrayHolder(){
		$this->db->saveExec($this->insertColorCombination);
		return $this->pdo->lastInsertId();
	}
	public function insertSymbolIdInArrayHolder($symbolId,$costId){
		$this->combinationId=$costId;
		$this->colorId = $symbolId;
		$this->db->saveExec($this->insertColorInCombinations);
	}
	public function getCorrectColorCombinationId(array $colors){
		$colorsAsSTR = json_encode($colors);
		if( ($this->cache[$colorsAsSTR] ?? "" ) !== "" ){
			return $this->cache[$colorsAsSTR];
		}
		if(! ($colors || count($colors))){
			$res = $this->pdo->query("
				SELECT ColorCombinations.id
				FROM ColorCombinations
				WHERE ColorCombinations.id NOT IN (
					SELECT ColorsInCombinations.combinationId
					FROM ColorsInCombinations
					GROUP BY ColorsInCombinations.combinationId
				)
				LIMIT 1
			")->fetch(PDO::FETCH_ASSOC);
			if(!$res){
				$id = $this->makeArrayHolder();
				$this->cache[$colorsAsSTR] = $id;
				return $id;
			}
			$this->cache[$colorsAsSTR] = $res["id"];
			return $res["id"];

		}
		//$splited = array_map([$this,"getColorId"],$colors);
		$asIds = [];//$colors;
		foreach($colors as $key=>$value){
			$this->colorSymbol = $value;
			$this->db->saveExec($this->symbolToId);
			$id = $this->symbolToId->fetch(PDO::FETCH_ASSOC)["id"] ?? false;
			if(!$id){
				$this->db->saveExec($this->insertColorSymbol);
				$id = $this->pdo->lastInsertId();
			}
			$asIds[] = $id;
		}
		$allPossible = array();
		$first = true;
		foreach($asIds as $key=>$id){
			if($first){
				$allPossible = $this->getColorArrayContainingColor($id);  //
				$first=false;
			} else {
				$allPossible = $this->somethingElse($allPossible,$id);
			}
			if( count($allPossible) == 0 ){
				$lineId = $this->makeArrayHolder();
				foreach($asIds as $key=>$newIds){
					$this->insertSymbolIdInArrayHolder($newIds,$lineId);
				}
				$this->cache[$colorsAsSTR] = $lineId;
				return $lineId;
			}
		}
		foreach($allPossible as $key=>$possible){
			$this->combinationId = $possible;
			$this->db->saveExec($this->getColorsInColorCombination);
			$ids = $this->getColorsInColorCombination->fetchAll(PDO::FETCH_ASSOC);
			if(count($ids) === count($asIds)){
				$this->cache[$colorsAsSTR] = $possible;
				return $possible;
			}
		}
		$lineId = $this->makeArrayHolder();
		foreach($asIds as $key=>$newIds){
			$this->insertSymbolIdInArrayHolder($newIds,$lineId);
		}
		$this->cache[$colorsAsSTR] = $lineId;
		return $lineId;

	}
}
