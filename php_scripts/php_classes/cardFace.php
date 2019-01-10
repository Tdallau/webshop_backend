<?php
class cardFace {
	public function __construct(db $db){
		$this->db = $db;
		$this->pdo = $db->pdo;

		$this->checkIfFaceExists = $this->pdo->prepare("SELECT id FROM CardFaces WHERE name = :name AND cardId=:oracleId");
		$this->checkIfFaceExists->bindParam(":name",$this->faceName);
		$this->checkIfFaceExists->bindParam(":oracleId",$this->oracleId);

		$this->insertFace = $this->pdo->prepare("
			INSERT INTO CardFaces
			(
				cardId,
				name,
				oracleText,
				power,
				toughness,
				loyalty,
				manaCostId,
				colorid,
				typeLineid
			) VALUES (
				:cardId,
				:name,
				:oracleText,
				:power,
				:toughness,
				:loyalty,
				:manaCostId,
				:colorId,
				:typeLineid
			)
			ON DUPLICATE KEY UPDATE
				cardId=     :cardId,
				name=       :name,
				oracleText= :oracleText,
				power=      :power,
				toughness=  :toughness,
				loyalty=    :loyalty,
				manaCostId= :manaCostId,
				colorId=    :colorId,
				typeLineid=  :typeLineid
			"
		);
		$this->insertFace->bindParam(":cardId"     ,$this->oracleId);
		$this->insertFace->bindParam(":name"       ,$this->faceName);
		$this->insertFace->bindParam(":oracleText" ,$this->oracleText);
		$this->insertFace->bindParam(":power"      ,$this->power);
		$this->insertFace->bindParam(":toughness"  ,$this->toughness);
		$this->insertFace->bindParam(":loyalty"    ,$this->loyalty);
		$this->insertFace->bindParam(":manaCostId" ,$this->manaCostId);
		$this->insertFace->bindParam(":colorId"    ,$this->colorId);
		$this->insertFace->bindParam(":typeLineid"    ,$this->typeLineId);
	}
	public function getAllCardFacesFromCard(stdClass $card){
		foreach( ($card->card_faces ?? []) as $key=>$value){
			$card->card_faces[$key]->colors = $card->card_faces[$key]->colors ?? $card->colors;
			$card->card_faces[$key]->image_uris = $card->card_faces[$key]->image_uris ?? $card->image_uris;
		}
		if($card->card_faces ?? false){
			return $card->card_faces;
		} else {
			return [$card];
		}
	}
	public function insertCardFace(stdClass $cardFace, string $oracleId,int $costId, int $colorId,int $typeLineId){
		$this->faceName = $cardFace->name;
		$this->oracleId = $oracleId;
		$this->oracleText = $cardFace->oracle_text;
		$this->power = $cardFace->power ?? null;
		$this->toughness = $cardFace->toughness ?? null;
		$this->loyalty = $cardFace->loyalty ?? null;
		$this->manaCostId = $costId;
		$this->colorId = $colorId;
		$this->typeLineId = $typeLineId;
		return $this->db->fetchOrInsert($this->checkIfFaceExists,$this->insertFace);
	}
}
