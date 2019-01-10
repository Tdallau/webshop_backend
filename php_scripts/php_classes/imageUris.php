<?php
class imageUris {
	public function __construct(db $db){
		$this->db = $db;
		$this->pdo = $db->pdo;

		$this->insertCardFace = $this->pdo->prepare("
			REPLACE INTO ImagesUrl (
				`printFaceid`,
				`small`,
				`normal`,
				`large`,
				`png`,
				`art_crop`,
				`border_crop`
			) VALUES (
				:printFaceid,
				:small,
				:normal,
				:large,
				:png,
				:art_crop,
				:border_crop
			)");
		$this->insertCardFace->bindParam("printFaceid",$this->printFaceid);
		$this->insertCardFace->bindParam("small",$this->small);
		$this->insertCardFace->bindParam("normal",$this->normal);
		$this->insertCardFace->bindParam("large",$this->large);
		$this->insertCardFace->bindParam("png",$this->png);
		$this->insertCardFace->bindParam("art_crop",$this->art_crop);
		$this->insertCardFace->bindParam("border_crop",$this->border_crop);
	}
	public function insert($cardFace,$printFaceId){
		$this->printFaceid=$printFaceId;
		$this->small=$cardFace->image_uris->small;
		$this->normal=$cardFace->image_uris->normal;
		$this->large=$cardFace->image_uris->large;
		$this->png=$cardFace->image_uris->png;
		$this->art_crop=$cardFace->image_uris->art_crop;
		$this->border_crop=$cardFace->image_uris->border_crop;
		$this->db->saveExec($this->insertCardFace);
	}
}
