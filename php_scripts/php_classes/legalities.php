<?php
class legalities {
	public function __construct(db $db){
		$this->db = $db;
		$this->pdo = $db->pdo;

		$this->checkIfLegalStatusExist = $this->pdo->prepare("SELECT id FROM Legalities WHERE name=:statusName");
		$this->checkIfLegalStatusExist->bindParam(":statusName",$this->statusName);

		$this->insertLegalStatus = $this->pdo->prepare("INSERT INTO Legalities (name) VALUES (:statusName)");
		$this->insertLegalStatus->bindParam(":statusName",$this->statusName);
		
		$this->checkIfLegalitieExists = $this->pdo->prepare("
			SELECT id
			FROM Legalitie
			WHERE `brawlid`   = :brawlid
			AND `commanderid` = :commanderid
			AND `duelid`      = :duelid
			AND `frontierid`  = :frontierid
			AND `futureid`    = :futureid
			AND `legacyid`    = :legacyid
			AND `modernid`    = :modernid
			AND `one_v_oneid` = :one_v_oneid
			AND `pauperid`    = :pauperid
			AND `pennyid`     = :pennyid
			AND `standardid`  = :standardid
			AND `vintageid`   = :vintageid
		");
		$this->checkIfLegalitieExists->bindParam(":brawlid"     , $this->brawlid);
		$this->checkIfLegalitieExists->bindParam(":commanderid" , $this->commanderid);
		$this->checkIfLegalitieExists->bindParam(":duelid"      , $this->duelid);
		$this->checkIfLegalitieExists->bindParam(":frontierid"  , $this->frontierid);
		$this->checkIfLegalitieExists->bindParam(":futureid"    , $this->futureid);
		$this->checkIfLegalitieExists->bindParam(":legacyid"    , $this->legacyid);
		$this->checkIfLegalitieExists->bindParam(":modernid"    , $this->modernid);
		$this->checkIfLegalitieExists->bindParam(":one_v_oneid" , $this->one_v_oneid);
		$this->checkIfLegalitieExists->bindParam(":pauperid"    , $this->pauperid);
		$this->checkIfLegalitieExists->bindParam(":pennyid"     , $this->pennyid);
		$this->checkIfLegalitieExists->bindParam(":standardid"  , $this->standardid);
		$this->checkIfLegalitieExists->bindParam(":vintageid"   , $this->vintageid);

		$this->insertLegalitie = $this->pdo->prepare("
			INSERT INTO Legalitie
			(
				`brawlid`,
				`commanderid`,
				`duelid`,
				`frontierid`,
				`futureid`,
				`legacyid`,
				`modernid`,
				`one_v_oneid`,
				`pauperid`,
				`pennyid`,
				`standardid`,
				`vintageid`
			) VALUES (
				:brawlid,
				:commanderid,
				:duelid,
				:frontierid,
				:futureid,
				:legacyid,
				:modernid,
				:one_v_oneid,
				:pauperid,
				:pennyid,
				:standardid,
				:vintageid
			)
		");
		$this->insertLegalitie->bindParam(":brawlid"     , $this->brawlid);
		$this->insertLegalitie->bindParam(":commanderid" , $this->commanderid);
		$this->insertLegalitie->bindParam(":duelid"      , $this->duelid);
		$this->insertLegalitie->bindParam(":frontierid"  , $this->frontierid);
		$this->insertLegalitie->bindParam(":futureid"    , $this->futureid);
		$this->insertLegalitie->bindParam(":legacyid"    , $this->legacyid);
		$this->insertLegalitie->bindParam(":modernid"    , $this->modernid);
		$this->insertLegalitie->bindParam(":one_v_oneid" , $this->one_v_oneid);
		$this->insertLegalitie->bindParam(":pauperid"    , $this->pauperid);
		$this->insertLegalitie->bindParam(":pennyid"     , $this->pennyid);
		$this->insertLegalitie->bindParam(":standardid"  , $this->standardid);
		$this->insertLegalitie->bindParam(":vintageid"   , $this->vintageid);
	}
	
	public function legalNameToId(string $name){
		$this->statusName = $name;
		return $this->db->fetchOrInsert($this->checkIfLegalStatusExist,$this->insertLegalStatus);
	}
	public function getLegalId(stdClass $card){
		$this->brawlid     = $this->legalNameToId($card->legalities->brawl);
		$this->commanderid = $this->legalNameToId($card->legalities->commander);
		$this->duelid      = $this->legalNameToId($card->legalities->duel);
		$this->frontierid  = $this->legalNameToId($card->legalities->frontier);
		$this->futureid    = $this->legalNameToId($card->legalities->future);
		$this->legacyid    = $this->legalNameToId($card->legalities->legacy);
		$this->modernid    = $this->legalNameToId($card->legalities->modern);
		$this->one_v_oneid = $this->legalNameToId($card->legalities->{"1v1"});
		$this->pauperid    = $this->legalNameToId($card->legalities->pauper);
		$this->pennyid     = $this->legalNameToId($card->legalities->penny);
		$this->standardid  = $this->legalNameToId($card->legalities->standard);
		$this->vintageid   = $this->legalNameToId($card->legalities->vintage);
		return $this->db->fetchOrInsert($this->checkIfLegalitieExists, $this->insertLegalitie);
		
	}
}
