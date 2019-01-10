<?php
spl_autoload_register(function($className){
	require("./php_classes/".$className . ".php");
});
$db= new db();
$insertSet = $db->pdo->prepare("
	INSERT INTO `Set`(
		`Id`,
		`name`,
		`setType`,
		`releasedAt`,
		`blockId`,
		`paretnSetCode`,
		`cardCount`,
		`foilOnly`,
		`iconSVG`
	) VALUES (
		:id,
		:name,
		:setType,
		:releasedAt,
		:blockId,
		:paretnSetCode,
		:cardCount,
		:foilOnly,
		:iconSVG
	)
	ON DUPLICATE KEY UPDATE
		`name`          = :name,
		`setType`       = :setType,
		`releasedAt`    = :releasedAt,
		`blockId`       = :blockId,
		`paretnSetCode` = :paretnSetCode,
		`cardCount`     = :cardCount,
		`foilOnly`      = :foilOnly,
		`iconSVG`       = :iconSVG
");
$insertSet->bindParam(":id",$id);
$insertSet->bindParam(":name",$name);
$insertSet->bindParam(":setType",$setType);
$insertSet->bindParam(":releasedAt",$releasedAt);
$insertSet->bindParam(":blockId",$blockId);
$insertSet->bindParam(":paretnSetCode",$paretnSetCode);
$insertSet->bindParam(":cardCount",$cardCount);
$insertSet->bindParam(":foilOnly",$foilOnly);
$insertSet->bindParam(":iconSVG",$iconSVG);

$insertBlock = $db->pdo->prepare("
	INSERT INTO `Block` (
		`id`,
		`name`
	) VALUES (
		:id,
		:name
	)
	ON DUPLICATE KEY UPDATE
		name=:name
");
$insertBlock->bindParam(":id",$blockId);
$insertBlock->bindParam(":name",$blockName);
$blockTable = array();

$ch = curl_init();
// set url
curl_setopt($ch, CURLOPT_URL, "https://api.scryfall.com/sets/");

//return the transfer as a string
curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);

// $output contains the output string
$output = json_decode(curl_exec($ch));

// close curl resource to free up system resources
curl_close($ch);
foreach($output->data as $k=>$set){
	var_dump($k);
	var_dump($set);
	$db->pdo->beginTransaction();
	if( ($set->block_code ?? false) && ! ($blockTable[$set->block_code] ?? false)){
		$blockId = $set->block_code;
		$blockName = $set->block;
		$db->saveExec($insertBlock);
		$blockTable[$set->block_code] = true;
	}
	$timestamp = null;
	if($set->released_at){
		$a = strptime($set->released_at, '%Y-%m-%d');
		$timestamp = mktime(0, 0, 0, $a['tm_mon']+1, $a['tm_mday'], $a['tm_year']+1900);
	}
	$id=            $set->code;
	$name=          $set->name;
	$setType=       $set->set_type;
	$releasedAt=    $timestamp;
	$blockId=       $set->block_code ?? null;
	$paretnSetCode= $set->parent_set_code ?? null;
	$cardCount=     $set->card_count;
	$foilOnly=      $set->foil_only;
	$iconSVG=       $set->icon_svg_uri;
	$db->saveExec($insertSet);
	$db->pdo->commit();
}
