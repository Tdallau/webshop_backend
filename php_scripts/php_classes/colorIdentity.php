<?php
class colorIdentity extends BasicColorTable {
	public function __construct(db $db){
		$this->db = $db;
		$this->pdo = $db->pdo;
		$this->costInsert = $this->pdo->prepare("INSERT INTO ColorsInIdentity () VALUES ()");

		$this->symbolCostInsert = $this->pdo->prepare("INSERT INTO ColorsInIdentity (colorId,identityid) VALUES (:symbolId,:costId)");
		$this->symbolCostInsert->bindParam(":symbolId",$this->symbolId);
		$this->symbolCostInsert->bindParam(":costId",$this->costId);

		$this->symbolToId = $this->pdo->prepare("SELECT id FROM Color INNER JOIN CostSymbols ON CostSymbols.id = Color.symbolid WHERE strSymbol= :symbolSTR");
		$this->symbolToId->bindParam(":symbolSTR",$this->symbolSTR);

		$this->getSymbolsInCost = $this->pdo->prepare("SELECT colorid AS id FROM ColorsInIdentity WHERE identityid = :costId");
		$this->getSymbolsInCost->bindParam(":costId",$this->costId);
	}
	public function getPossibleSymbolArrays(array $symbolArray){
		$str = '
			SELECT id FROM ColorIdentity WHERE ColorIdentity.id IN (
				SELECT Color.id
				FROM ColorsInIdentity
				INNER JOIN Color
				ON Color.id= ColorsInIdentity.colorId
				WHERE Color.symbolid IN({ARRAY})
			)
		';
		return parent::runSQL($str,$symbolArray);
	}
	public function insertSymbolArray(array $symbolArray){
		return parent::runInsertQueries($this->costInsert,$this->symbolCostInsert,$symbolArray);
	}
	public function getAllSymbolsInSymbolArray(int $possibleSymbolArrayId){
		$this->costId = $possibleSymbolArrayId;
		$this->db->saveExec($this->getSymbolsInCost);
		return $this->getSymbolsInCost->fetchAll(PDO::FETCH_ASSOC);
	}
	public function getCorrectColorIdentitytId(color_arrays $colorArrays, array $strCosts){
		$colorsToSymbols = array_map(function($val){return "{".$val."}";},$strCosts);
		return $colorArrays->createOrGetSymbolArrayUsingArray($colorsToSymbols,$this);
	}
}
