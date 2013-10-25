import Entities.Customer;

import java.rmi.registry.LocateRegistry;
import java.rmi.registry.Registry;
import java.util.Scanner;

public class ClientMain {
    private static void ShowAll(CorpIsRmiInterface service) throws Exception
    {
        for (Customer customer : service.getAllCustomers()) {
            System.out.println(String.format("%d %s %f", customer.getId(), customer.getName(), customer.getBalance()));
        }

    }

    public static void main(String[] args) throws Exception {
        Registry registry = LocateRegistry.getRegistry("localhost", 2099);
        CorpIsRmiInterface service = (CorpIsRmiInterface) registry.lookup("sample/HelloService");
        String command = "";
        Scanner  input = new Scanner(System.in);

        System.out.println(String.format("Commands: sh, ch"));
        while(command != "exit")
        {
            command = input.nextLine();
            if(command.equals("sh"))
            {
                ShowAll(service);
            }
            else if(command.equals("ch"))
            {
                System.out.println(String.format("Customer ID:"));
                int id = Integer.parseInt(input.nextLine());
                System.out.println(String.format("Sum:"));
                int sum = Integer.parseInt(input.nextLine());
                service.changeBalance(id, sum);
                ShowAll(service);
            }
        /*else if(command.equals("adda"))
            {
            System.out.print("add after: ");
            String target = input.nextLine();
            System.out.print("value = ");
            try{
                list.addAfter(list.find(target), input.nextLine());
            }
            catch (ElementNotFoundException e){

                System.out.print("Element not found.");
            }
        }   */
        }
    }
}
