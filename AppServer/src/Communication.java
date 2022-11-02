import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.net.ServerSocket;
import java.net.Socket;
public class Communication {

    public static void main(String[] args)
    {
        Communication communication = new Communication();
    }
    int port = 8909;
    Socket dal_socket;
    public Communication() {

        try{
            listen_to_DataBase();
        }
        catch (IOException e)
        {

        }

    }

    public void listen_to_DataBase() throws IOException {
        ServerSocket serverSocket = new ServerSocket(port);
        Socket socket = serverSocket.accept();
        this.dal_socket = socket;
        while(true)
        {
            InputStream input = this.dal_socket.getInputStream();
            BufferedReader reader = new BufferedReader(new InputStreamReader(input));
            String line = reader.readLine();
            System.out.println(line);
        }
    }






}
