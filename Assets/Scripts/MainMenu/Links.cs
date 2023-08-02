using System.Collections.Generic;
public static class Links
{
    public static Dictionary<string, string> links = new Dictionary<string, string>();
    static public string ip;
    public static void SetLinks()
    {
        links["logout"] = $"http://{ip}/unity/_functions/RegLog/logout.php";
        links["login"] = $"http://{ip}/unity/_functions/RegLog/login.php";
        links["reg"] = $"http://{ip}/unity/_functions/RegLog/reg.php";
        links["update"] = $"http://{ip}/unity/_functions/Servers/update.php";
        links["create"] = $"http://{ip}/unity/_functions/Servers/create.php";
        links["connect"] = $"http://{ip}/unity/_functions/Servers/connect.php";
        links["checkOpponent"] = $"http://{ip}/unity/_functions/Online/checkOpponent.php";
        links["checkClass"] = $"http://{ip}/unity/_functions/Online/checkClass.php";
        links["setRole"] = $"http://{ip}/unity/_functions/Online/setRole.php";
        links["read"] = $"http://{ip}/unity/_functions/Online/readData.php";
        links["write"] = $"http://{ip}/unity/_functions/Online/writeData.php";
        links["disconnectGame"] = $"http://{ip}/unity/_functions/Servers/disconnectGame.php";
        links["disconnect"] = $"http://{ip}/unity/_functions/Servers/disconnect.php";
    }
}
