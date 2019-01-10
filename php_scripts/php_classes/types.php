<?php
class types {
	public function __construct(db $db){
		$this->db = $db;
		$this->pdo = $db->pdo;

		$this->typeLineInsert = $this->pdo->prepare("INSERT INTO TypeLine () VALUES ()");
		$this->typesInLineInsert = $this->pdo->prepare("INSERT INTO TypesInLine (lineid,typeid) VALUES (:lineid,:typeid)");
		$this->typesInLineInsert->bindParam(":lineid",$this->lineId);
		$this->typesInLineInsert->bindParam(":typeid",$this->typeId);

		$this->typeNameToId = $this->pdo->prepare("SELECT id FROM Types WHERE typeName = :typeName");
		$this->typeNameToId->bindParam(":typeName",$this->typeName);

		$this->typeInsert = $this->pdo->prepare("INSERT INTO Types (typeName) VALUES (:typeName)");
		$this->typeInsert->bindParam(":typeName",$this->typeName);

		$this->getTypesInLine = $this->pdo->prepare("SELECT typeid AS id FROM TypesInLine WHERE lineid = :lineId");
		$this->getTypesInLine->bindParam(":lineId",$this->lineId);

		$this->getTypeLinesBySingleType = $this->pdo->prepare("SELECT TypeLine.id FROM TypeLine,TypesInLine WHERE TypesInLine.typeId=:typeId AND TypesInLine.lineId=TypeLine.id");
		$this->getTypeLinesBySingleType->bindParam(":typeId",$this->typeId);
		$this->cache = array();

	}
	private function split($str, $len = 1) {
		$arr = [];
		$length = mb_strlen($str, 'UTF-8');
		for ($i = 0; $i < $length; $i += $len) {
			$arr[] = mb_substr($str, $i, $len, 'UTF-8');
		}
		return $arr;

	}
	private function splitTypeString(string $typeLine){
		$rawArray = $this->split($typeLine);
		$typeArray =array("");
		$at = 0;
		$didSpace = false;
		array_walk($rawArray, function($char) use (&$typeArray,&$at,&$didSpace){
			if($char==" "){
				if(!$didSpace){
					$typeArray[$at] = trim($typeArray[$at]);
					$at = $at +1;
					$typeArray[$at] = "";
					$didSpace= true;
				}
			} else {
				$didSpace = false;
			}
			$typeArray[$at] = $typeArray[$at] . $char;
		});
		return array_map(function($v){return strtolower(trim($v));},$typeArray);
	}
	public function something(int $typeId){
		$this->typeId = $typeId;
		$this->db->saveExec($this->getTypeLinesBySingleType);
		return array_map(function($v){return $v["id"];},$this->getTypeLinesBySingleType->fetchAll(PDO::FETCH_ASSOC));
	}
	public function somethingElse(array $lineIds,$typeId){
		$query = "
			SELECT TypeLine.id
			FROM TypeLine
			INNER JOIN TypesInLine
			ON TypesInLine.lineId=TypeLine.id
			WHERE TypeLine.id IN ({array})
			AND TypesInLine.TypeId = $typeId
		";
		$lineIdsAsString = implode($lineIds,",");
		$queryReplaced = str_replace("{array}",$lineIdsAsString,$query);
		$res = $this->pdo->query($queryReplaced);
		if(!$res){
			echo $queryReplaced;
		}
		return array_map(function($v){return $v["id"];},$res->fetchAll(PDO::FETCH_ASSOC));
	}
	public function makeArrayHolder(){
		$this->db->saveExec($this->typeLineInsert);
		return $this->pdo->lastInsertId();

	}
	public function insertTypeIdInArrayHolder($typeId,$lineId){
		$this->lineId=$lineId;
		$this->typeId = $typeId;
		$this->db->saveExec($this->typesInLineInsert);
	}
	public function getCorrectTypeLineId(string $strTypeLine){
		if( ($this->cache[$strTypeLine] ?? "") !== "" ){
			return $this->cache[$strTypeLine];
		}
		$splited = $this->splitTypeString($strTypeLine);
		$asIds = [];
		foreach($splited as $key=>$value){
			$this->typeName = $value;
			$this->db->saveExec($this->typeNameToId);
			$id = $this->typeNameToId->fetch(PDO::FETCH_ASSOC)["id"] ?? false;
			if(!$id){
				$this->db->saveExec($this->typeInsert);
				$id = $this->pdo->lastInsertId();
			}
			$asIds[] = $id;
		}

		$allPossible = array();
		$first = true;
		foreach($asIds as $key=>$id){
			if($first){
				$allPossible = $this->something($id);
				$first=false;
			} else {
				$allPossible = $this->somethingElse($allPossible,$id);
			}
			if( count($allPossible) ==0 ){
				$lineId = $this->makeArrayHolder();
				foreach($asIds as $key=>$newIds){
					$this->insertTypeIdInArrayHolder($newIds,$lineId);
				}
				$this->cache[$strTypeLine] = $lineId;
				return $lineId;
			}
		}
		foreach($allPossible as $key=>$possible){
			$this->lineId = $possible;
			$this->db->saveExec($this->getTypesInLine);
			$ids = $this->getTypesInLine->fetchAll(PDO::FETCH_ASSOC);
			if(count($ids) === count($asIds)){
				$this->cache[$strTypeLine] = $possible;
				return $possible;
			}
		}
		$lineId = $this->makeArrayHolder();
		foreach($asIds as $key=>$newIds){
			$this->insertTypeIdInArrayHolder($newIds,$lineId);
		}
		$this->cache[$strTypeLine] = $lineId;
		return $lineId;

	}
	/*
	public function getPossibleArrays(array $symbolArray){
		$str = '
			SELECT id FROM TypeLine WHERE TypeLine.id IN (
				SELECT TypesInLine.lineid
				FROM TypesInLine
				WHERE TypesInLine.TypeId IN({ARRAY})
			)
		';
		return parent::runSQL($str,$symbolArray);
	}
	public function insertArray(array $symbolArray){
		return parent::runInsertQueries($symbolArray);
	}
	public function arrayInsert(){
		$this->db->saveExec($this->typeLineInsert);
	}
	public function setArrayHolder($id){
		$this->lineId = $id;
	}
	public function elementInsert($element){
		$this->typeId = $element;
		$this->db->saveExec($this->typesInLineInsert);
	}
	public function getAllElementsInArray(int $possibleSymbolArrayId){
		$this->typeId = $possibleSymbolArrayId;
		$this->db->saveExec($this->getTypesInLine);
		$res = $this->getTypesInLine->fetchAll(PDO::FETCH_ASSOC);
		return $res;
	}
	public function getTypeId(string $type){
		$this->typeName = trim($type);
		$this->db->saveExec($this->typeNameToId);
		$id = $this->typeNameToId->fetch(PDO::FETCH_ASSOC)["id"] ?? false;
		if(!$id){
			$this->db->saveExec($this->typeInsert);
			return $this->pdo->lastInsertId();
		}
		return $id;
	}
	private function split($str, $len = 1) {
		$arr = [];
		$length = mb_strlen($str, 'UTF-8');
		for ($i = 0; $i < $length; $i += $len) {
			$arr[] = mb_substr($str, $i, $len, 'UTF-8');
		}
		return $arr;

	}
	private function splitTypeString(string $typeLine){
		$rawArray = $this->split($typeLine);
		$typeArray =array("");
		$at = 0;
		$didSpace = false;
		array_walk($rawArray, function($char) use (&$typeArray,&$at,&$didSpace){
			if($char==" "){
				if(!$didSpace){
					$typeArray[$at] = trim($typeArray[$at]);
					$at = $at +1;
					$typeArray[$at] = "";
					$didSpace= true;
				}
			} elseif($didSpace && $char=="—"){
				return;
			} else {
				$didSpace = false;
			}
			$typeArray[$at] = $typeArray[$at] . $char;
		});
		return array_map(function($v){return strtolower(trim($v));},$typeArray);
	}
	public function getCorrectTypeLineId(arrayInterface $arrayInterface, string $strTypeLine){
		$typeArray = $this->splitTypeString($strTypeLine);
		$typeArray2 =array_map([$this,"getTypeId"],$typeArray);
		if(count($typeArray2)==0){
			echo "its empty";
			var_dump($typeArray);
			var_dump($strTypeLine);
		}
		return $arrayInterface->getArrayId($typeArray2,$this,true);
	}
	* */
}
