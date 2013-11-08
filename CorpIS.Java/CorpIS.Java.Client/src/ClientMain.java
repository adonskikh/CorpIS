
import java.util.Scanner;

import javacorba.CorbaServiceModule.CorbaService;
import javacorba.CorbaServiceModule.CorbaServiceHelper;
import org.omg.CosNaming.*;
import org.omg.CORBA.*;

public class ClientMain {

    private static void ShowAll() throws Exception {
        for (int customerId : service.getAllCustomerIds()) {
            String customerName = service.getName(customerId);
            float customerBalance = service.getBalance(customerId);
            System.out.println(String.format("%d %s %f", customerId, customerName, customerBalance));
        }
    }

    static CorbaService service;

    public static void main(String[] args) throws Exception {

        try {
            // create and initialize the ORB
            ORB orb = ORB.init(args, null);

            // get the root naming context
            org.omg.CORBA.Object objRef = orb.resolve_initial_references("NameService");
            // Use NamingContextExt instead of NamingContext. This is
            // part of the Interoperable naming Service.
            NamingContextExt ncRef = NamingContextExtHelper.narrow(objRef);

            // resolve the Object Reference in Naming
            String name = "CorbaService";
            service = CorbaServiceHelper.narrow(ncRef.resolve_str(name));

        } catch (Exception e) {
            System.out.println("ERROR : " + e);
            e.printStackTrace(System.out);
        }

        String command = "";
        Scanner input = new Scanner(System.in);

        System.out.println(String.format("Commands: sh, ch"));
        while (command != "exit") {
            command = input.nextLine();
            if (command.equals("sh")) {
                ShowAll();
            } else if (command.equals("ch")) {
                System.out.println(String.format("Customer ID:"));
                int id = Integer.parseInt(input.nextLine());
                System.out.println(String.format("Sum:"));
                int sum = Integer.parseInt(input.nextLine());
                service.changeBalance(id, sum);
                ShowAll();
            }
        }
    }
}
