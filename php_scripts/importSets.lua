local req = require"requests"
local DBI = require"DBI"
local pos = require"posix.time"
local term  = require"term"
local dbh = assert(DBI.Connect('MySQL', "projectC", "C#","C#", "localhost", 3306))

local insertSet = assert(dbh:prepare([[
INSERT INTO `Set`(
	`id`,
	`name`,
	`setType`,
	`releasedAt`,
	`blockId`,
	`paretnSetCode`,
	`cardCount`,
	`foilOnly`,
	`iconSVG`
) VALUES (
	?,
	?,
	?,
	?,
	?,
	?,
	?,
	?,
	?
)
ON DUPLICATE KEY UPDATE
	`name`=?,
	`setType`=?,
	`releasedAt`=?,
	`blockId`=?,
	`paretnSetCode`=?,
	`cardCount`=?,
	`foilOnly`=?,
	`iconSVG`=?
]]))
local insertBlock = assert(dbh:prepare([[
INSERT INTO `Block` (
	`id`,
	`name`
) VALUES (
	?,
	?
)
ON DUPLICATE KEY UPDATE
	name=?
]]))
local blockTable = {}
local function saveRun(sth,...)
    assert(sth:execute(...))
    return sth
end

local setRes = req.get('https://api.scryfall.com/sets/').json()

for k,set in ipairs(setRes.data) do
	term.cursor.jump(1,1)
	term.clear()
	local splitTime = {}
	local timeStamp = nil
	if set.released_at then
		for numb in string.gmatch(set.released_at or "", "%d+")  do
			table.insert(splitTime,numb)
		end
		timeStamp = os.time{
			year  = splitTime[1],
			month = splitTime[2],
			day   = splitTime[3]
		}
	else
		timeStamp = 0
	end
	io.write(
		"code: ",set.code,"\n",
		"Set: ", set.name,"\n",
		"Block: ",set.block_code or "","\n",
		"count: ", k,"\n",
		"timeStamp: ",timeStamp,"\n",
		"releaseDate: ", set.released_at or "","\n"
	)

	if set.block_code and not blockTable[set.block_code] then
		saveRun(insertBlock, set.block_code,set.block,set.block)
		blockTable[set.block_code] = true
	end
	saveRun(
		insertSet,
		set.code,
		set.name,
		set.setType,
		timeStamp,
		set.block_code,
		set.parent_set_code,
		set.card_count,
		set.foil_only,
		set.icon_svg_uri,
		set.name,
		set.setType,
		timeStamp,
		set.block_code,
		set.parent_set_code,
		set.card_count,
		set.foil_only,
		set.icon_svg_uri
	)
	dbh:commit()

end
