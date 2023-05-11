import org.json.JSONArray;
import org.json.JSONObject;
public class Jsonifier {
    public static JSONObject toJSONObject (String data) {
        try {
            return new JSONObject(data);
        }
        catch (Exception e) {
            System.err.println("Error while converting String to JSON Object! ");
            e.printStackTrace(System.err);
            return null;
        }
    }
    public static JSONArray getArrayNamed (JSONObject json, String arrayName) {
        try {
            return (JSONArray) json.get(arrayName);
        }
        catch (Exception e) {
            System.err.println("Error while searching for JSON Array! ");
            e.printStackTrace(System.err);
            return null;
        }
    }
}
