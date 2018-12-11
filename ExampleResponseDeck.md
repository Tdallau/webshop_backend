GET -> mainurl/api/decks hiermee krijg je al je decks je hoeft niks mee te geven.

### response voorbeeld

```json
{
    "data": [
        {
            "id": 3,
            "name": "Henk",
            "image": "https://img.scryfall.com/cards/art_crop/en/tsp/157.jpg?1517813031",
            "fullImage": "https://img.scryfall.com/cards/normal/en/tsp/157.jpg?1517813031"
        },
        {
            "id": 5,
            "name": "Test",
            "image": "https://img.scryfall.com/cards/art_crop/en/tsp/157.jpg?1517813031",
            "fullImage": "https://img.scryfall.com/cards/normal/en/tsp/157.jpg?1517813031"
        },
        {
            "id": 6,
            "name": "Test",
            "image": "https://img.scryfall.com/cards/art_crop/en/tsp/157.jpg?1517813031",
            "fullImage": "https://img.scryfall.com/cards/normal/en/tsp/157.jpg?1517813031"
        }
    ],
    "success": true
}
```

GET -> mainurl/api/decks/{deckId}
hier krijg je een bepaald deck(door het deckId(hoofdletter I) mee te geven) 
je krijg  

### response voorbeeld

```json
{
    "data": {
        "id": 5,
        "name": "Test",
        "image": "https://img.scryfall.com/cards/art_crop/en/tsp/157.jpg?1517813031",
        "fullImage": "https://img.scryfall.com/cards/normal/en/tsp/157.jpg?1517813031",
        "cards": [
            {
                "id": "00154b70-57d2-4c32-860f-1c36fc49b10c",
                "name": "Destructive Tampering",
                "image": "https://img.scryfall.com/cards/small/en/aer/78.jpg?1517813031",
                "flavorText": "\"I don't think they'll appreciate my . . . adjustments.\" —Karavin, renegade saboteur",
                "oracleText": "Choose one —\n• Destroy target artifact.\n• Creatures without flying can't block this turn.",
                "loyalty": null,
                "power": null,
                "toughness": null,
                "price": 4,
                "typeLine": "sorcery",
                "mana": [
                    {
                        "id": 8,
                        "strSymbol": "{2}",
                        "picturePath": null
                    },
                    {
                        "id": 14,
                        "strSymbol": "{R}",
                        "picturePath": null
                    }
                ]
            },
            {
                "id": "0014def3-4063-4929-ac51-76aef1bb2a68",
                "name": "Shahrazad",
                "image": "https://img.scryfall.com/cards/small/en/arn/10.jpg?1534549398",
                "flavorText": null,
                "oracleText": "Players play a Magic subgame, using their libraries as their decks. Each player who doesn't win the subgame loses half their life, rounded up.",
                "loyalty": null,
                "power": null,
                "toughness": null,
                "price": 38478,
                "typeLine": "sorcery",
                "mana": [
                    {
                        "id": 10,
                        "strSymbol": "{W}",
                        "picturePath": null
                    }
                ]
            }
        ]
    },
    "success": true
}
```

POST -> mainurl/api/decks
hier kan je een nieuw deck aanmaken

###  request body voorbeeld

```json
{
	"Name" : "nieuwe test",
	"Commander" : "0014def3-4063-4929-ac51-76aef1bb2a68",
	"SecondaryCommander" : "000d609c-deb7-4bd7-9c1d-e20fb3ed4f5f"
}
```

Hier van dient commander en SecondaryCommander een printId te zijn(SecondaryCommander is optioneel)

### response voorbeeld

```json
{
    "data": "Deck is created!!",
    "success": true
}
```

POST -> mainurl/api/decks/addCard
hier kan je een een nieuwe kaart toevoegen aan je deck

###  request body voorbeeld

```json
{
	"printId" : "0000579f-7b35-4ed3-b44c-db2a538066fe",
	"deckId" : 5
}
```

### response voorbeeld

```json
{
    "data": "Card is added to your deck",
    "success": true
}
```