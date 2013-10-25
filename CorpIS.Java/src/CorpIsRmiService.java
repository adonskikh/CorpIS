import Entities.Customer;
import org.hibernate.ejb.HibernatePersistence;

import javax.persistence.EntityManager;
import javax.persistence.EntityManagerFactory;
import javax.persistence.EntityTransaction;
import javax.persistence.spi.PersistenceProvider;
import java.rmi.RemoteException;
import java.rmi.server.ServerNotActiveException;
import java.rmi.server.UnicastRemoteObject;
import java.util.HashMap;
import java.util.List;

/**
 * Created with IntelliJ IDEA.
 * User: Artyom
 * Date: 24.10.13
 * Time: 23:49
 * To change this template use File | Settings | File Templates.
 */
public class CorpIsRmiService implements CorpIsRmiInterface {

    private EntityManager _entityManager;

    public CorpIsRmiService()
    {
        PersistenceProvider provider = new HibernatePersistence();
        EntityManagerFactory entityManagerFactory = provider.createEntityManagerFactory("NewPersistenceUnit", new HashMap());
        _entityManager = entityManagerFactory.createEntityManager();
        List<Customer> customerList = _entityManager.createQuery("from Customer").getResultList();
    }

    public Object sayHello(String name) {
        String string = "Hello, " + name + "! It is " + System.currentTimeMillis() + " ms now";
        try {
            System.out.println(name + " from " + UnicastRemoteObject.getClientHost());
        } catch (ServerNotActiveException e) {
            System.out.println(e.getMessage());
        }
        if ("Killer".equals(name)) {
            System.out.println("Shutting down...");
            System.exit(1);
        }
        return string;
    }

    @Override
    public Customer getCustomer(int id) throws RemoteException {
        return _entityManager.find(Customer.class, id);
    }

    @Override
    public float changeBalance(int id, float diff) throws RemoteException {
        Customer customer =  _entityManager.find(Customer.class, id);
        if(customer != null)
        {
            EntityTransaction tr = _entityManager.getTransaction();
            tr.begin();
            customer.setBalance(customer.getBalance() + diff);
            _entityManager.flush();
            tr.commit();
            return customer.getBalance();
        }
        return 0;
    }

    @Override
    public List<Customer> getAllCustomers() throws RemoteException {
        return _entityManager.createQuery("from Customer").getResultList();
    }
}