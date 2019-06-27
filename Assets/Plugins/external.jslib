mergeInto(LibraryManager.library, {

    GetNumberFromUnity: function () {
        return unityNumber;
    },

    SetNumberFromUnity: function (s) {
        unityNumber = s;
    },

    GetHighScoreFromCookie: function () {
        var data = document.cookie.split(";");

        data.forEach(function (datum) {
            var contents = value.split("=");
            if (contents[0] === "atelier144-infiniteblocks-highscore") {
                return contents[1];
            }
        });
        return 0;
    },

    SetHighScoreToCookie: function (highScore) {
        document.cookie = "atelier144-infiniteblocks-highscore=" + highScore + "";
    },

    GetUnlockedLevelFromCookie: function () {
        return 0;
    },

    SetUnlockedLevelToCookie: function (unlockedLevel) {
        document.cookie = "atelier144-infiniteblocks-unlockedlevel=" + unlockedLevel + "";
    }
});
