<?php
$content = json_decode(file_get_contents("scryfall-all-cards.json"));
array_walk($content,function($card){
	if(file_exists("./splitted/". $card->id . ".json")){
		var_dump($card);
		echo "File already exists!";
		die();
	}
	file_put_contents("./splitted/". $card->id . ".json", json_encode($card));
});
