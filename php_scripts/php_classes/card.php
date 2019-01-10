<?php
class card {
	public function __construct(db $db){
		$this->db = $db;
		$this->pdo = $db->pdo;
		$this->checkIfCardExists = $this->pdo->prepare("SELECT * FROM Card WHERE id = :oracleId");
		$this->checkIfCardExists->bindParam(":oracleId",$this->oracleId);

		$this->insertCard = $this->pdo->prepare("
			INSERT INTO `Card`
			(
				`Id`,
				`EdhrecRank`,
				`colorIdentityid`,
				`legalitiesid`
			) VALUES (
				:oracleId,
				:EdhrecRank,
				:colorIdentityid,
				:legalitiesid
			)
			ON DUPLICATE KEY UPDATE
				EdhrecRank=:EdhrecRank,
				colorIdentityid=:colorIdentityid,
				legalitiesid=:legalitiesid
			"
		);
		$this->insertCard->bindParam(":oracleId",$this->oracleId);
		$this->insertCard->bindParam(":EdhrecRank",$this->EdhrecRank);
		$this->insertCard->bindParam(":colorIdentityid",$this->colorIdentityid);
		$this->insertCard->bindParam(":legalitiesid",$this->legalitiesid);
	}
	public function insertCard(stdClass $card, int $legalId, int $colorIdentityId){
		$this->oracleId = $card->oracle_id;

		$this->db->saveExec($this->checkIfCardExists);
		$this->colorIdentityid = $colorIdentityId;
		$res = $this->checkIfCardExists->fetch(PDO::FETCH_ASSOC);
		if(!$res){
			$this->EdhrecRank = $card->edhrec_rank ?? null;
			$this->legalitiesid = $legalId;
			$this->db->saveExec($this->insertCard);
		}
	}
}
