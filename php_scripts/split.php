<?php
rmdir("./splitted/");
mkdir("./splitted/");
$ch = curl_init();
// set url
curl_setopt($ch, CURLOPT_URL, "https://archive.scryfall.com/json/scryfall-all-cards.json");

//return the transfer as a string
curl_setopt($ch, CURLOPT_RETURNTRANSFER, 1);

// $output contains the output string
$content = json_decode(curl_exec($ch));

// close curl resource to free up system resources
curl_close($ch);
array_walk($content,function($card){
	if(file_exists("./splitted/". $card->id . ".json")){
		var_dump($card);
		echo "File already exists!";
		die();
	}
	file_put_contents("./splitted/". $card->id . ".json", json_encode($card));
});
rmdir("./splitted/");
