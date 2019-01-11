<?php
class cardPrint{
	public function __construct(db $db){
		$this->db =$db;
		$this->pdo = $db->pdo;
		$this->insertPrintSQL = $this->pdo->prepare("
			INSERT INTO `Print`(
				`Id`,
				`CardId`,
				`price`,
				`foil`,
				`nonfoil`,
				`oversized`,
				`borderColor`,
				`collectorsNumber`,
				`fullArt`,
				`setId`,
				`languageid`
			) VALUES (
				:Id,
				:CardId,
				:price,
				:foil,
				:nonfoil,
				:oversized,
				:borderColor,
				:collectorsNumber,
				:fullArt,
				:setId,
				:languageid
			)
			ON DUPLICATE KEY UPDATE
				CardId=           :CardId,
				foil=             :foil,
				nonfoil=          :nonfoil,
				oversized=        :oversized,
				borderColor=      :borderColor,
				collectorsNumber= :collectorsNumber,
				fullArt=          :fullArt,
				setId=            :setId,
				languageid=       :languageid
		"
		);
		$this->insertPrintSQL->bindParam(":Id",$this->Id);
		$this->insertPrintSQL->bindParam(":CardId",$this->CardId);
		$this->insertPrintSQL->bindParam(":price",$this->price);
		$this->insertPrintSQL->bindParam(":foil",$this->foil);
		$this->insertPrintSQL->bindParam(":nonfoil",$this->nonfoil);
		$this->insertPrintSQL->bindParam(":oversized",$this->oversized);
		$this->insertPrintSQL->bindParam(":borderColor",$this->borderColor);
		$this->insertPrintSQL->bindParam(":collectorsNumber",$this->collectorsNumber);
		$this->insertPrintSQL->bindParam(":fullArt",$this->fullArt);
		$this->insertPrintSQL->bindParam(":setId",$this->setId);
		$this->insertPrintSQL->bindParam(":languageid",$this->languageid);
		$this->insertLanguage = $this->pdo->prepare("
			INSERT INTO Languages (
				code
			) VALUES (
				:code
			)
		");
		$this->insertLanguage->bindParam(":code",$this->langCode);

		$this->getLanguage = $this->pdo->prepare("SELECT Id FROM Languages WHERE code = :code");
		$this->getLanguage->bindParam(":code",$this->langCode);
	}
	public function getLanguageId($langCode){
		$this->langCode = $langCode;
		return $this->db->fetchOrInsert($this->getLanguage,$this->insertLanguage);
	}
	public function insertPrint($card){
		$price = null;
		if($card->eur ?? false){
			$price = $card->eur * 100;
		}
		var_dump($card);
		$this->Id = $card->id;
		$this->CardId=$card->oracle_id;
		$this->price=$price;
		$this->foil=$card->foil;
		$this->nonfoil=$card->nonfoil;
		$this->oversized=$card->oversized;
		$this->borderColor=$card->border_color;
		$this->collectorsNumber=$card->collector_number;
		$this->fullArt=$card->full_art;
		$this->setId=$card->set;
		$this->languageid=$this->getLanguageId($card->lang);
		$this->db->saveExec($this->insertPrintSQL);
		return $card->id;
	}
}
