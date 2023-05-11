import org.json.JSONArray;
import org.json.JSONObject;

public class main {
    public static void main(String[] args) {
        Api api = new Api("http://localhost");
        String source = api.requestCities();
        JSONArray recieveddata = Jsonifier.getArrayNamed(Jsonifier.toJSONObject(source), "cities");
        for (Object i: recieveddata) {
            System.out.println("City ID: " + ((JSONObject) i).get("ID"));
            System.out.println("City name: " + ((JSONObject) i).get("Name"));
            System.out.println("Country: " + ((JSONObject) i).get("CountryCode"));
            System.out.println("District: " + ((JSONObject) i).get("District"));
            System.out.println("Population: " + ((JSONObject) i).get("Population"));
        }
    }
}
