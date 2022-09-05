// WARNING: Do not modify! Generated file.

namespace UnityEngine.Purchasing.Security {
    public class GooglePlayTangle
    {
        private static byte[] data = System.Convert.FromBase64String("6+PLdRSjbgSG5Ie/k8PvJRRzHes7rNXuBRDGEVjFVFv5aMuzjypeNnpk7vFutOw8kSpAycvJN7pczmE6yUpES3vJSkFJyUpKS+koIpF7OQp7yUppe0ZNQmHNA828RkpKSk5LSCgz0Ol9s4dGgOcHlinMH6uIsPBRqvn3s481GjMnz8bzNmD0Lm4T8dQ9FCXkHSybUvERsHC9WuKCxaNDnhDlgywH+6sukh/mfG1Y3QKKgiT7CbVMPJdzXlUBuumQrSCZ5w7LlqkCNzccOApPeiDJlRw9RFWAHtZcIA6lunfWyZBl8POVe5OUdVXPYG0mKvgJHR964BBZrNq30aiqK8Mh4emsM2HUOLqKyvDEBJgogvoHGShjhWpDdtFAYfv0BklISktK");
        private static int[] order = new int[] { 8,9,11,9,8,10,12,11,12,11,12,12,12,13,14 };
        private static int key = 75;

        public static readonly bool IsPopulated = true;

        public static byte[] Data() {
        	if (IsPopulated == false)
        		return null;
            return Obfuscator.DeObfuscate(data, order, key);
        }
    }
}
