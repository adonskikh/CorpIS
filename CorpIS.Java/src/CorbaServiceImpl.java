import javacorba.CorbaServiceModule.CorbaServicePOA;
import org.omg.CORBA.ORB;

import java.util.Iterator;
import java.util.List;

/**
 * Created with IntelliJ IDEA.
 * User: Artyom
 * Date: 09.11.13
 * Time: 2:11
 * To change this template use File | Settings | File Templates.
 */
public class CorbaServiceImpl extends CorbaServicePOA {
    private ORB orb;
    private CorpRepository service = new CorpRepository();

    public void setORB(ORB orb_val) {
        orb = orb_val;
    }

    @Override
    public float changeBalance(int id, float diff) {
        try {
            return service.changeBalance(id, diff);  //To change body of implemented methods use File | Settings | File Templates.
        } catch (Exception e) {
        }
        return -1;
    }

    @Override
    public String getName(int id) {
        try {
            Entities.Customer customer = service.getCustomer(id);
            System.out.println(customer.getName());
            return customer.getName(); //To change body of implemented methods use File | Settings | File Templates.
        } catch (Exception e) {
        }
        return null;
    }

    @Override
    public float getBalance(int id) {
        try {
            Entities.Customer customer = service.getCustomer(id);
            return customer.getBalance(); //To change body of implemented methods use File | Settings | File Templates.
        } catch (Exception e) {
        }
        return -1;
    }

    @Override
    public int[] getAllCustomerIds() {
        try {
            List<Entities.Customer> customers = service.getAllCustomers();
            int[] result = new int[customers.size()];
            int i = 0;
            for (Iterator<Entities.Customer> customerIt = customers.iterator(); customerIt.hasNext(); ) {
                result[i] = customerIt.next().getId();
                ++i;
            }
            return result; //To change body of implemented methods use File | Settings | File Templates.
        } catch (Exception e) {
        }
        return null;
    }
}
