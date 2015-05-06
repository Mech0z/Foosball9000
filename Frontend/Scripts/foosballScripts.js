


var setLocalTimeOnMatch = function (matches) {
    for (var i = 0; i < matches.length; i++) {
        matches[i].LocalTime = toLocalFormat(matches[i].TimeStampUtc);
    }
    return ;
};

var toLocalFormat = function (date) {

    return dateFormat(date, "dd/mmmm/yy HH:MM");
};

var setupUsers = function (leaderboard, users) {

    for (var i = 0; i < leaderboard.length; i++) {
        for (var j = 0; j < users.length; j++) {
            if (leaderboard[i].UserName === users[j].Email) {
                leaderboard[i].displayName = users[j].Username;
            }
        }
    }

    return;
};

var setupUsersMatches = function (matches, users) {
    for (var i = 0; i < matches.length; i++) {
        for (var j = 0; j < users.length; j++) {
            for (var p = 0; p < matches[i].PlayerList.length; p++) {
                if (matches[i].PlayerList[p] === users[j].Email) {
                    matches[i].PlayerList[p] = users[j].Username;
                }
            }
        }
    }
    return;
};


