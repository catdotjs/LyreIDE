using System.Collections.Generic;

public static class StackExtension{
    // Thank you to Finickyflame from stack overflow for this solution
    public static List<T> PopRange<T>(this Stack<T> stack, int amount){
        List<T> result = new List<T>(amount);
        for(int i=0;i<amount;i++){
            stack.Pop();
        }
        return result;
    }
}