using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Assertions;

namespace Lomztein.BFA2.Cheats
{
    public class CheatCode
    {
        private static List<CheatCode> _cheats = new List<CheatCode>();

        public enum EncryptionMethod { Raw, Caeser, Sha256 }

        public readonly string Code;
        public readonly EncryptionMethod Encryption;
        public readonly Action Execute;

        public static event Action<CheatCode, string> OnCheatActivated;

        public CheatCode(string code, EncryptionMethod encryption, Action execute)
        {
            Assert.IsTrue(code == code.ToLower(), "Cheat codes should be in lower case.");
            Code = code;
            Encryption = encryption;
            Execute = execute;
        }

        public CheatCode(string code, Action execute) : this(code, EncryptionMethod.Raw, execute) { }

        public static void RegisterCheat(CheatCode cc) => _cheats.Add(cc);
        public static void RegisterCheat(string code, EncryptionMethod encryption, Action execute)
            => RegisterCheat(new CheatCode(code, encryption, execute));

        public static void RegisterCheat(string code, Action execute)
            => RegisterCheat(code, EncryptionMethod.Raw, execute);

        public static bool TryExecuteCheat(string code)
        {
            string trimmed = code.TrimEnd(';');
            int repeatCount = code.Length - trimmed.Length + 1;

            foreach (var cheat in _cheats)
            {
                if (Matches(trimmed, cheat.Code, cheat.Encryption))
                {
                    for (int i = 0; i < repeatCount; i++)
                    {
                        cheat.Execute();
                    }
                    OnCheatActivated?.Invoke(cheat, code);
                    return true;
                }
            }
            return false;
        }

        private static bool Matches(string input, string code, EncryptionMethod method)
        {
            input = input.ToLower();
            var encrypt = Encrypt(input, method);
            return encrypt.Equals(code);
        }

        public static string Encrypt(string input, EncryptionMethod method)
        {
            // Could be a strategy pattern, but I'm a bit sick of those right now.
            switch (method)
            {
                case EncryptionMethod.Raw:
                    return input;

                case EncryptionMethod.Caeser:
                    return new string(input.Select(x => (char)(x + input.Length)).ToArray());

                case EncryptionMethod.Sha256:
                    return string.Concat(SHA256.Create().ComputeHash(Encoding.ASCII.GetBytes(input)).Select(x => x.ToString("x2")));
            }
            throw new NotImplementedException("The selected method has not been implemented yet.");
        }
    }
}
