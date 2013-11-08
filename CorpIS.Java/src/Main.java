import Entities.Customer;
import javacorba.CorbaServiceModule.CorbaService;
import javacorba.CorbaServiceModule.CorbaServiceHelper;
import org.hibernate.ejb.HibernatePersistence;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.spi.PersistenceProvider;
import java.rmi.Remote;
import java.rmi.registry.LocateRegistry;
import java.rmi.registry.Registry;
import java.rmi.server.UnicastRemoteObject;
import java.util.HashMap;
import java.util.List;

import org.omg.CosNaming.*;
import org.omg.CORBA.*;
import org.omg.PortableServer.*;
import org.omg.PortableServer.POA;

public class Main {

    public static void main(String[] args) throws Exception {
        // write your code here
        System.out.print("Starting registry...");
/*        final Registry registry = LocateRegistry.createRegistry(2099);
        System.out.println(" OK");

        CorpIsRmiInterface service = new CorpRepository();
        Remote stub = UnicastRemoteObject.exportObject(service, 0);*/


        try {
            // create and initialize the ORB
            ORB orb = ORB.init(args, null);

            // get reference to rootpoa & activate the POAManager
            POA rootpoa = POAHelper.narrow(orb.resolve_initial_references("RootPOA"));
            rootpoa.the_POAManager().activate();

            // create servant and register it with the ORB
            CorbaServiceImpl BSImpl = new CorbaServiceImpl();
            BSImpl.setORB(orb);

            // get object reference from the servant
            org.omg.CORBA.Object ref = rootpoa.servant_to_reference(BSImpl);
            CorbaService href = CorbaServiceHelper.narrow(ref);

            // get the root naming context
            // NameService invokes the name service
            org.omg.CORBA.Object objRef = orb.resolve_initial_references("NameService");
            // Use NamingContextExt which is part of the Interoperable
            // Naming Service (INS) specification.
            NamingContextExt ncRef = NamingContextExtHelper.narrow(objRef);

            // bind the Object Reference in Naming
            String name = "CorbaService";
            NameComponent path[] = ncRef.to_name(name);
            ncRef.rebind(path, href);

            System.out.println("BillingServiceServer ready and waiting ...");

            // wait for invocations from clients
            orb.run();
        } catch (Exception e) {
            System.err.println("ERROR: " + e);
            e.printStackTrace(System.out);
        }


/*        System.out.print("Binding service...");
        registry.bind("sample/HelloService", stub);
        System.out.println(" OK");*/

        while (true) {
            Thread.sleep(Integer.MAX_VALUE);
        }

    }
}
