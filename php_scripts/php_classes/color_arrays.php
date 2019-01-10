<?php
class color_arrays {
	public function __construct(db $db){
		$this->db = $db;
		$this->pdo = $db->pdo;
		
		$this->symbolToId = $this->pdo->prepare("SELECT id FROM CostSymbols WHERE strSymbol= :symbolSTR");
		$this->symbolToId->bindParam(":symbolSTR",$this->symbolSTR);

		$this->symbolInsert = $this->pdo->prepare("INSERT INTO CostSymbols (strSymbol) VALUES (:symbolSTR)");
		$this->symbolInsert->bindParam(":symbolSTR",$this->symbolSTR);
	}
	public function getSymbolId(string $symbol){
		$this->symbolSTR = $symbol;
		$this->db->saveExec($this->symbolToId);
		$id = $this->symbolToId->fetch(PDO::FETCH_ASSOC)["id"] ?? false;
		if(!$id){
			$this->db->saveExec($this->symbolInsert);
			return $this->pdo->lastInsertId();
		}
		return $id;
	}
	private function splitSymbolString(string $symbolstr){
		$rawArray = str_split($symbolstr);
		$symbolArray =array();
		$at = -1;
		array_walk($rawArray, function($char) use (&$symbolArray,&$at){
			if($char=="{"){
				$at = $at +1;
				$symbolArray[$at] = "";
			}
			$symbolArray[$at] = $symbolArray[$at] . $char;
		});
		return $symbolArray;
	}
	public function createSymbolCompareDicts(array $symbolArray, array $extraCompare= null){
		$newCompareSymbolArray = array();
		foreach($symbolArray as $key=>$symbolId){
			if($extraCompare){
				if(is_array($symbolId)){
					$symbolId = $symbolId["id"];
				}
				if(empty($extraCompare[$symbolId])){
					return false;
				}
			}
			if(empty($newCompareSymbolArray[$symbolId] )){
				$newCompareSymbolArray[$symbolId] = 0;
			}
			$newCompareSymbolArray[$symbolId]++;
		}
		return $newCompareSymbolArray;
	}
	public function createOrGetSymbolArray(string $symbolSTR,IColorTable $tableInterface){
		$symbolArray = $this->splitSymbolString($symbolSTR);
		return $this->createOrGetSymbolArrayUsingArray($symbolArray,$tableInterface);
	}
	public function createOrGetSymbolArrayUsingArray(array $symbolArray, IColorTable $tableInterface){
		$symbolArray =array_map([$this,"getSymbolId"],$symbolArray);
		$res = $tableInterface->getPossibleSymbolArrays($symbolArray);
		$rowCount = $res->rowCount();
		if ($rowCount==0){
			return $tableInterface->insertSymbolArray($symbolArray);
		}
		
		$compareCorrectSymbolDict = $this->createSymbolCompareDicts($symbolArray);

		$possibleSymbolArrays = $res->fetchAll(PDO::FETCH_ASSOC);
		foreach($possibleSymbolArrays as $key=>$possibleSymbolArray){
			$symbolIdsInPossibleSymbolArray = $tableInterface->getAllSymbolsInSymbolArray($possibleSymbolArray["id"]);
			$comapreMaybeCorrectSymbolDict = $this->createSymbolCompareDicts($symbolIdsInPossibleSymbolArray,$compareCorrectSymbolDict);
			if($comapreMaybeCorrectSymbolDict === false){
				continue;
			}
			if(count($comapreMaybeCorrectSymbolDict) != count($compareCorrectSymbolDict)){
				continue;
			}
			foreach($comapreMaybeCorrectSymbolDict as $symbolId=>$count){
				if($compareCorrectSymbolDict[$symbolId] != $count){
					continue;
				}
			}
			return $possibleSymbolArray["id"];
		}
		return  $tableInterface->insertSymbolArray($symbolArray);// $this->insertSymbol($symbols);
	}
}
