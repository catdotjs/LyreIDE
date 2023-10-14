using System;
using System.Linq;

namespace Lyre.Extends;
public static class StringExtension{
    /// <summary>
    /// Replaces given array of characters with replacement character
    /// </summary>
    /// <param name="characters">Array of characters TO replace</param>
    /// <param name="replacement">Character to replace WITH</param>
    /// <returns>string</returns>
    /// <exception cref="ArgumentException">characters cannot contain replacement character!</exception>
    public static string ReplaceMultiple(this string str,char[] characters,char replacement){
        if(characters.Contains(replacement)){ 
            throw new ArgumentException($"Replacement character cannot be in replacing array! {replacement} was in given array!");
        }

        string result = str;
        foreach(char chr in characters){
            result = result.Replace(chr,replacement);
        }

        return result;
    }
}