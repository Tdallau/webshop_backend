<?php
class printFace {
	public function __construct(db $db){
		$this->db = $db;
		$this->pdo = $db->pdo;
		$this->insertPrintFace = $this->pdo->prepare("
			INSERT INTO PrintFace(PrintId,flavorText)
			VALUES (:PrintId,:flavorText)
			ON DUPLICATE KEY UPDATE
				flavorText=:flavorText
		");
		$this->insertPrintFace->bindParam(":PrintId",$this->printId);
		$this->insertPrintFace->bindParam(":flavorText",$this->flavorText);
	}
	public function insertPrintFace($cardFace,$printId){
		$this->flavorText = $cardFace->flavor_text ?? null;
		$this->printId = $printId;
		$this->db->saveExec($this->insertPrintFace);
		return $this->pdo->lastInsertId();
	}
}
