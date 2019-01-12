<?php
$host = '127.0.0.1';
$db   = 'ProjectC';
$user = 'C#';
$pass = 'C#';

$dsn = "mysql:host=$host;dbname=$db;";
$pdo = new PDO($dsn, $user, $pass);

$getSymbol = $Pdo->prepare("SELECT id from CostSymbols WHERE strSymbol = :symbol");
$getSymbol->bindParam(":symbol",$getSymbol);

function getCorrectCostId(PDO $pdo, array $preps , array $costs){
    $str = '
        SELECT id FROM Costs WHERE Costs.id IN (
            SELECT SymbolsInCosts.costid
            FROM SymbolsInCosts
            WHERE SymbolsInCosts IN [{ARRAY}]
        )
    ';
    $str = str_replace("{ARRAY}",implode(",",$pdo->quote($costs)),$str);
    $res = $pdo->query($str);
    if($res){
        if($res->rowCount > 1) {
            echo "SOMETHING WEND HORRIBLY WRONG!";
            var_dump($sth->fetchAll(PDO::FETCH_ASSOC));
            die();
        }
        return $res->fetch(PDO::FETCH_ASSOC);
    } else {
        if($preps["createCost"]->execute()){
            $id = $pdo->lastInsertId();
            foreach($costs as $key => $value){
                if(!$preps["createInCosts"]->execute([":symbolId"=>$value,":costId"=>$id])){
                     $pdo->rollBack();
                    var_dump($sth->errorInfo());
                    die();
                }
            }
            return $id;
        }
    }
}
$costPreps = [
    "createInCosts" => $pdo->prepare("INSERT INTO SymbolsInCosts (symbolid,costid) VALUES (:symbolId,:costId)"),
    "createCost" => $pdo->prepare("INSERT INTO Costs")
];

$checkInCosts = $pdo->prepare('
    SELECT id FROM Costs WHERE Costs.id IN (
        SELECT SymbolsInCosts.costid
        FROM SymbolsInCosts
        WHERE SymbolsInCosts IN {ARRAY}
    )
');
$Product = $pdo->prepare('
    INSERT INTO `Product`(
        `id`,
        `lang`,
        `oracle_id`,
        `foil`,
        `loyalty`,
        `mana_cost`,
        `name`,
        `nonfoil`,
        `oracle_text`,
        `power`,
        `reserved`,
        `toughness`,
        `type_line`,
        `price`,
        `rarity`,
        `set`,
        `setName`
    ) VALUES (
        :id,
        :lang,
        :oracle_id,
        :foil,
        :loyalty,
        :mana_cost,
        :name,
        :nonfoil,
        :oracle_text,
        :power,
        :reserved,
        :toughness,
        :type_line,
        :price,
        :rarity,
        :set,
        :setName
    )'
);
$Product->bindParam(":id",$id);
$Product->bindParam(":lang",$lang);
$Product->bindParam(":oracle_id",$oracle_id);
$Product->bindParam(":foil",$foil);
$Product->bindParam(":loyalty",$loyalty);
$Product->bindParam(":mana_cost",$mana_cost);
$Product->bindParam(":name",$name);
$Product->bindParam(":nonfoil",$nonfoil);
$Product->bindParam(":oracle_text",$oracle_text);
$Product->bindParam(":power",$power);
$Product->bindParam(":reserved",$reserved);
$Product->bindParam(":toughness",$toughness);
$Product->bindParam(":type_line",$type_line);
$Product->bindParam(":price",$price);
$Product->bindParam(":rarity",$rarity);
$Product->bindParam(":set",$set);
$Product->bindParam(":setName",$setName);
/*
$ColorIdentity = $pdo->prepare("
    INSERT INTO `ColocolorIndicator`(
        `productId`, `colorid`
    ) VALUES (
        :productId, :colorid
    )");
$ColorIdentity->bindParam(":productId",$id);
$ColorIdentity->bindParam(":colorid",$colorId);
*/
$image = $pdo->prepare('
    INSERT INTO `ImagesUrl`(
        `productId`,
        `small`,
        `normal`,
        `large`,
        `png`,
        `art_crop`,
        `border_crop`
    ) VALUES (
        :productId,
        :small,
        :normal,
        :large,
        :png,
        :art_crop,
        :border_crop
    )
');
$image->bindParam(":productId",$id);
$image->bindParam(":small",$small);
$image->bindParam(":normal",$normal);
$image->bindParam(":large",$large);
$image->bindParam(":png",$png);
$image->bindParam(":art_crop",$art_crop);
$image->bindParam(":border_crop",$border_crop);
$legilite = $pdo->prepare(
    'INSERT INTO `Legalitie`(
        `productId`,
        `standard`,
        `future`,
        `frontier`,
        `modern`,
        `legacy`,
        `pauper`,
        `vintage`,
        `penny`,
        `commander`,
        `one_v_one`,
        `duel`,
        `brawl`
    ) VALUES (
        :productId,
        :standard,
        :future,
        :frontier,
        :modern,
        :legacy,
        :pauper,
        :vintage,
        :penny,
        :commander,
        :one_v_one,
        :duel,
        :brawl
    )
    '
);
$legilite->bindParam(":productId",$id);
$legilite->bindParam(":standard",$standard);
$legilite->bindParam(":future",$future);
$legilite->bindParam(":frontier",$frontier);
$legilite->bindParam(":modern",$modern);
$legilite->bindParam(":legacy",$legacy);
$legilite->bindParam(":pauper",$pauper);
$legilite->bindParam(":vintage",$vintage);
$legilite->bindParam(":penny",$penny);
$legilite->bindParam(":commander",$commander);
$legilite->bindParam(":one_v_one",$one_v_one);
$legilite->bindParam(":duel",$duel);
$legilite->bindParam(":brawl",$braw);
echo "Read content:";
$content = json_decode(file_get_contents("scryfall-all-cards.json"));
echo "Done reading content";
$goodExec = function( PDOStatement $sth) use ($pdo){
    $success = $sth->execute();
    if(!$success){
        $pdo->rollBack();
        var_dump($sth->errorInfo());
        throw new Exception("db error");
    }
};
$total = count($content);
foreach($content as $key=>$value){
    $pdo->beginTransaction();
    $name = $value->name;
    $id=$value->id ;
    $lang=$value->lang;
    $oracle_id=$value->oracle_id;
    $foil=$value->foil;
    $loyalty=$value->loyalty ?? null;
    $mana_cost=$value->mana_cost;
    $nonfoil=$value->nonfoil;
    $oracle_text=$value->oracle_text ?? null;
    $power=$value->power ?? null;
    $reserved=$value->reserved;
    $toughness=$value->toughness ?? null;
    $type_line=$value->type_line;
    $price=null;//$value->;
    $rarity=$value->rarity;
    $set=$value->set;
    $setName=$value->set_name;
    $goodExec($Product);
    $small=$value->image_uris->small;
    $normal=$value->image_uris->normal;
    $large=$value->image_uris->large;
    $png=$value->image_uris->png;
    $art_crop=$value->image_uris->art_crop;
    $border_crop=$value->image_uris->border_crop;
    $goodExec($image);

    $standard=$value->legalities->standard;
    $future=$value->legalities->future;
    $frontier=$value->legalities->frontier;
    $modern=$value->legalities->modern;
    $legacy=$value->legalities->legacy;
    $pauper=$value->legalities->pauper;
    $vintage=$value->legalities->vintage;
    $penny=$value->legalities->penny;
    $commander=$value->legalities->commander;
    $one_v_one=$value->legalities->{"1v1"};
    $duel=$value->legalities->duel;
    $braw=$value->legalities->brawl;
    $goodExec($legilite);
    $pdo->commit();
    if($key % 100==0){
        var_dump($value);
        echo "We are at $key / $total";
    }
    unset($content->{$key});

}
