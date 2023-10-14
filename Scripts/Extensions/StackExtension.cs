using System.Collections.Generic;

namespace Lyre.Extends;
public static class StackExtension{
    // Thank you to Finickyflame from stack overflow for this solution
    /// <summary>
    /// Pop multiple items from the list
    /// </summary>
    /// <param name="amount">Ammount of members to pop from the stack</param>
    /// <returns>Stack<T></returns>
    public static List<T> PopRange<T>(this Stack<T> stack, int amount){
        List<T> result = new List<T>(amount);
        for(int i=0;i<amount;i++){
            stack.Pop();
        }
        return result;
    }
}