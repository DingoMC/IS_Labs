import java.io.InputStreamReader;
import java.io.InputStream;
import java.io.BufferedReader;
import java.net.URL;
import java.util.stream.Collectors;

public class Api {
    String base_url;
    public Api (String connection_url) {
        this.base_url = connection_url;
    }

    public String requestCityData (int city_id) {
        try {
            URL url = new URL(base_url + "/IS_LAB6_REST/cities/read/" + String.valueOf(city_id));
            System.out.println("Sending request..");
            InputStream is = url.openStream();
            System.out.println("Retrieving data...");
            return new BufferedReader(new InputStreamReader(is)).lines().collect(Collectors.joining("\n"));
        }
        catch (Exception e) {
            System.err.println("Request failed! ");
            e.printStackTrace(System.err);
            return null;
        }
    }
    public String requestCities () {
        try {
            URL url = new URL(base_url + "/IS_LAB6_REST/cities/read");
            System.out.println("Sending request..");
            InputStream is = url.openStream();
            System.out.println("Retrieving data...");
            return new BufferedReader(new InputStreamReader(is)).lines().collect(Collectors.joining("\n"));
        }
        catch (Exception e) {
            System.err.println("Request failed! ");
            e.printStackTrace(System.err);
            return null;
        }
    }
}
