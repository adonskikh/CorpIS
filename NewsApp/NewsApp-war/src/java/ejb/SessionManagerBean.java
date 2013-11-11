/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package ejb;

import javax.ejb.Singleton;
import javax.ejb.LocalBean;
import javax.servlet.annotation.WebListener;
import javax.servlet.http.HttpSessionEvent;
import javax.servlet.http.HttpSessionListener;

/**
 *
 * @author Artyom
 */
@Singleton
@LocalBean
@WebListener
public class SessionManagerBean implements HttpSessionListener {

    private static int count = 0;

    public static int getActiveSessionsCount() {
        return count;
    }

    @Override
    public void sessionCreated(HttpSessionEvent se) {
        count++;
    }

    @Override
    public void sessionDestroyed(HttpSessionEvent se) {
        count--;
    }
}
