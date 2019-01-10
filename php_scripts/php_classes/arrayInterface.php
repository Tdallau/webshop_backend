<?php
class arrayInterface{
	public function __construct(db $db){
		$this->db=$db;
		$this->pdo = $db->pdo;

	}
	public function createCompareDicts(array $toTransform, array $compareWith= null,bool $debug= false){
		$newCompareDict = array();
		foreach($toTransform as $key=>$element){
			if(is_array($element)){
				$element = $element["id"];
			}
			if($compareWith){
				if(empty($compareWith[$element])){
					return false;
				}
			}
			if(empty($newCompareDict[$element] )){
				$newCompareDict[$element] = 0;
			}
			$newCompareDict[$element]++;
		}
		return $newCompareDict;
	}
	public function getArrayId(array $targetArray, ITableArray $tableInterface, bool $debug = false){
		$res = $tableInterface->getPossibleArrays($targetArray);
		$rowCount = $res->rowCount();
		if ($rowCount==0){
			return $tableInterface->insertArray($targetArray);
		}

		$compareCorrectDict = $this->createCompareDicts($targetArray);

		$possibleArrays = $res->fetchAll(PDO::FETCH_ASSOC);
		foreach($possibleArrays as $key=>$possibleArray){
			$idsInPossibleArray = $tableInterface->getAllElementsInArray($possibleArray["id"]);
			$comapreMaybeCorrectDict = $this->createCompareDicts($idsInPossibleArray,$compareCorrectDict,$debug);
			if($comapreMaybeCorrectDict === false){
				if($debug){echo "its false\n";};
				continue;
			}
			if(count($comapreMaybeCorrectDict) != count($compareCorrectDict)){
				if($debug){echo "wrong count\n";};
				continue;
			}
			foreach($comapreMaybeCorrectDict as $arrayElementId=>$count){
				if($compareCorrectDict[$arrayElementId] != $count){
					if($debug){echo "Not correct id/count\n";};
					continue;
				}
			}
			if($debug){
				if($compareCorrectDict[1] ?? false){
					echo "we got it\n";
					echo "possibleArray ID", $possibleArray["id"],"\n";
					var_dump("as ids",$targetArray);
					var_dump("target",$this->test2($targetArray));
					var_dump("CompareMaybeCorrect",$this->test($comapreMaybeCorrectDict));
					var_dump("Corrext Dict",$this->test($compareCorrectDict));
				}


			}

			return $possibleArray["id"];
		}
		return  $tableInterface->insertArray($targetArray);// $this->insertSymbol($symbols);
	}
	function test2(array $mao){
		$test = $this->pdo->prepare("SELECT typeName FROM Types WHERE id=:id");
		$test->bindParam(":id",$val);
		$toReturn = [];
		foreach($map as $key=>$val){
			$this->db->saveExec($test);
			$toReturn[] = $test->fetch(PDO::FETCH_ASSOC)["typeName"];
		}
		return $toReturn;
	}
	function test(array $map){
		$test = $this->pdo->prepare("SELECT typeName FROM Types WHERE id=:id");
		$test->bindParam(":id",$key);
		$toReturn = [];
		foreach($map as $key=>$val){
			$this->db->saveExec($test);
			$toReturn[] = $test->fetch(PDO::FETCH_ASSOC)["typeName"];
		}
		return $toReturn;
	}


}
