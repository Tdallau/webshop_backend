<?php
spl_autoload_register(function($className){
	require("./php_classes/".$className . ".php");
});
/*
require("./php_classes/db.php");
require("./php_classes/costs.php");
require("./php_classes/cardFace.php");
require("./php_classes/card.php");
require("./php_classes/legalities.php");
*/

$db = new db();
$costs = new costs($db);
$cardFaceDB = new cardFace($db);
$cardDB = new card($db);
$legalities = new legalities($db);
$arrayTableInterface = new arrayInterface($db);
$colorArray = new colorArray($db);
$cardPrint = new cardPrint($db);
$types = new types($db);
$printFace =  new PrintFace($db);
$imageUris = new imageUris($db);

$files = glob("./splitted/*.json");
array_walk($files, function($cardFile,$key) use (
	&$content,
	$costs,
	$db,
	$cardFaceDB,
	$cardDB,
	$legalities,
	$colorArray,
	$arrayTableInterface,
	$cardPrint,
	$types,
	$printFace,
	$imageUris
	){
	$card = json_decode(file_get_contents($cardFile));
	$db->pdo->beginTransaction();
	echo $card->name,"\n";
	$legalId = $legalities->getLegalId($card);
	$colorIdentityid = $colorArray->getCorrectColorCombinationId($card->color_identity);
	$cardDB->insertCard($card,$legalId,$colorIdentityid);
	$cardFaces = $cardFaceDB->getAllCardFacesFromCard($card);
	$printId = $cardPrint->insertPrint($card);
	echo "doing faces : \n";
	foreach($cardFaces as $key=>$cardFace){
		echo $cardFace->name,"\n";
		$costId = $costs->getCorrectCostId($cardFace->mana_cost);
		$colorId = $colorArray->getCorrectColorCombinationId($cardFace->colors);
		$typeLineId = $types->getCorrectTypeLineId($cardFace->type_line);
		$cardFaceDB->insertCardFace($cardFace,$card->oracle_id,$costId,$colorId,$typeLineId);
		$printFaceId = $printFace->insertPrintFace($cardFace,$printId);
		$imageUris->insert($cardFace,$printFaceId);
	}
	unset($content[$key]);
	$db->pdo->commit();
});
echo "Done first pass";
unset($content);
$content = null;
$allCards = $db->pdo->query("SELECT id FROM `Card`")->fetchAll(PDO::FETCH_ASSOC);

$getPrints = $db->pdo->prepare('
	SELECT `Print`.Id FROM `Print`
	INNER JOIN `Set`
	ON Print.setId = `Set`.`Id`
	INNER JOIN Languages
	ON Print.languageid = Languages.id
	WHERE Languages.code = "en"
	AND `Print`.`CardId` = :cardId
	ORDER BY `Set`.`releasedAt` DESC
	LIMIT 1'
);

$getPrints->bindParam(":cardId",$cardId);

$update = $db->pdo->prepare("UPDATE Print SET isLatest=1 WHERE id=:printId");
$update->bindParam(":printId",$printId);

$removeLatest = $db->pdo->prepare("UPDATE Print SET isLatest=0 WHERE CardId = :cardId");
$removeLatest->bindParam(":cardId",$cardId);
foreach($allCards as $key=>$value){
	$cardId = $value["id"];
	$db->saveExec($getPrints);
	$card = $getPrints->fetch(PDO::FETCH_ASSOC);
	if($card){
		$printId = $card["Id"];
		$db->saveExec($removeLatest);
		$db->saveExec($update);
	}


}
