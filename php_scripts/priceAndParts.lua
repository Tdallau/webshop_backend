local req = require"requests"
local DBI = require"DBI"
local pos = require"posix.time"
local term  = require"term"
local dbh = assert(DBI.Connect('MySQL', "ProjectC", "C#","C#", "localhost", 3306))
local getNewCardQuery = dbh:prepare('SELECT * FROM Product WHERE Price = 0 LIMIT 1 ')
local updatePrice = dbh:prepare('UPDATE `Product` SET `price`=? WHERE id=?')
local insertPart = dbh:prepare('INSERT INTO `Parts`(`partOneId`, `partTwoId`) VALUES (?,?)')

local function saveRun(sth,...)
    assert(sth:execute(...))
    return sth
end
local done = false
local count = 0
local started = os.time()
local startedAsString = os.date("%H:%M:S",started)
repeat
    local startQueryTime = os.clock()
    local card = saveRun(getNewCardQuery):fetch(true)
    if card then
        count = count + 1
        local res = req.get('https://api.scryfall.com/cards/'..card.id)
        if res.status_code ~= 200 then
            error("Status code not 200, its" .. res.status_code)
        end
        local scryCard = res.json()
        local parts = {}
        if scryCard.all_parts then
            for k, v in ipairs(scryCard.all_parts) do
                table.insert(parts, v.name)
                saveRun(insertPart,card.id,v.id)
            end
        end
        saveRun(updatePrice,scryCard.eur,card.id)
        dbh:commit()
        local endQueryTime = os.clock()-startQueryTime
        if count % 10 == 0 or count ==1 then
            term.cursor.jump(1,1)
            term.clear()
            local totalTimePassed = os.difftime(os.time(),started)
            local secondsPassed =totalTimePassed
            local minutesPassed = 0
            local hoursPassed = 0
            if secondsPassed > 3600 then
                hoursPassed = math.floor(secondsPassed /3600)
                secondsPassed = secondsPassed - (hoursPassed * 3600)
            end
            if secondsPassed > 60 then
                minutesPassed = math.floor(secondsPassed/60)
                secondsPassed = secondsPassed - (minutesPassed * 60)
            end
            io.write(
                term.colors.green("Updated: "..count.."\n"),
                term.colors.green("Started at: "..startedAsString.."\n"),
                term.colors.green("Time passed: "..hoursPassed.." "..minutesPassed.." "..secondsPassed.."\n"),
                term.colors.green("Rows per second: "..count / totalTimePassed .."\n"),
                term.colors.red("Time needed to select card: ".. endQueryTime.."\n"),
                term.colors.magenta("ID: "..card.id.."\n"),
                term.colors.magenta("Name: "..scryCard.name.."\n"),
                term.colors.magenta("language: "..scryCard.lang.."\n"),
                term.colors.blue("Price: "..tostring(scryCard.eur).."\n"),
                term.colors.magenta("parts:\n"),
                term.colors.blue(table.concat(parts,"\n")) 
            )
        end
    else 
        done = true
    end
    pos.nanosleep({
        tv_sec = 0,
        tv_nsec = 60000000
    })
until done
