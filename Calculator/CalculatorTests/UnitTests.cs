using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator.CheckBook;
using System.Linq;
using System.Collections.ObjectModel;

namespace CalculatorTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod] //What is the average transaction amount for each tag?
        public void AverageTransactionAmount()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            var countF = ob.Transactions.Where(t => t.Tag == "Food").Count();
            var countA = ob.Transactions.Where(t => t.Tag == "Auto").Count();
            var totalF = ob.Transactions.Where(t => t.Tag == "Food").Sum(t => t.Amount);
            var totalA = ob.Transactions.Where(t => t.Tag == "Auto").Sum(t => t.Amount);
            var avgF = totalF / countF;
            var avgA = totalA / countA;

            Assert.AreEqual(32.625, avgF);
            Assert.AreEqual(75, avgA);
        }

        [TestMethod]  //How much did you pay each payee?
        public void PayeeAmount()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            var payment = ob.Transactions.GroupBy(t => t.Payee).Select(g => new { Sum = g.Sum(t => t.Amount) });

            Assert.AreEqual(130, payment.ElementAt(0).Sum);
            Assert.AreEqual(300, payment.ElementAt(1).Sum);
            Assert.AreEqual(131, payment.ElementAt(2).Sum);
        }

        [TestMethod]  //How much did you pay each payee for food?
        public void PayeeAmountFood()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            var paymentF = ob.Transactions.Where(l => l.Tag == "Food").GroupBy(t => t.Payee).Select(g => new { Sum = g.Sum(t => t.Amount) });

            Assert.AreEqual(130, paymentF.First().Sum);
            Assert.AreEqual(131, paymentF.Last().Sum);
        }

        [TestMethod]  //List the transaction between April 5th and 7th
        public void TransactionDatesBetween()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            DateTime firstDate = new DateTime(2015, 05, 04);
            DateTime secondDate = new DateTime(2015, 07, 04);

            var numDate = ob.Transactions.Where(t => t.Date >= firstDate).Where(t => t.Date <= secondDate).Select(t => new { t.Account, t.Payee, t.Amount, t.Tag });


            String[] acc = new String[] { "Checking", "Checking", "Credit", "Credit", "Credit", "Checking" };
            String[] p = new String[] { "Moshe", "Tim", "Moshe", "Bracha", "Tim", "Bracha" };
            String[] tags = new String[] { "Food", "Auto", "Food", "Food", "Auto", "Food" };
            double[] amt = new double[] { 30, 130, 30, 30.5, 130, 30.5 };

            for (int i = 0; i < numDate.Count(); i++)
            {
                Assert.AreEqual(acc[i], numDate.ElementAt(i).Account);
                Assert.AreEqual(p[i], numDate.ElementAt(i).Payee);
                Assert.AreEqual(amt[i], numDate.ElementAt(i).Amount);
                Assert.AreEqual(tags[i], numDate.ElementAt(i).Tag);
            }

        }

        [TestMethod]  //List the dates on which each account was used
        public void DateForAccount()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            var total =ob.Transactions.Count();
            var totalCheckA = ob.Transactions.Where(t => t.Account == "Checking");
            var totalCreditA = ob.Transactions.Where(t => t.Account == "Credit");

            var checkcount = totalCheckA.Count();
            var creditcount = totalCreditA.Count();

            DateTime[] dateCheck = new DateTime[] {new DateTime(2015, 07, 04),
                                              new DateTime(2015, 05, 04),
                                              new DateTime(2015, 04, 04),
                                              new DateTime(2015, 03, 04),
                                              new DateTime(2015, 02, 04),
                                              new DateTime(2015, 06, 04) };
            DateTime[] dateCredit = new DateTime[] {new DateTime(2015, 07, 04),
                                              new DateTime(2015, 06, 04),
                                              new DateTime(2015, 05, 04),
                                              new DateTime(2015, 04, 04),
                                              new DateTime(2015, 03, 04),
                                              new DateTime(2015, 02, 04)
                                             };
            for (var i = 0; i < checkcount; i++)
            {
                Assert.AreEqual(dateCheck[i], totalCheckA.ElementAt(i).Date);
            }
            for (var i = 0; i < creditcount; i++)
            {
                Assert.AreEqual(dateCredit[i], totalCreditA.ElementAt(i).Date);
            }
        }

        [TestMethod] //Which account was used most (amount of money) on auto expenses?
        public void AccountMaxAuto()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            var acc = ob.Transactions.Where(t => t.Tag == "Auto").Select(g => new { g.Account, g.Amount });
            var count = acc.Count();

            var MAX1 = acc.Where(a => a.Account == "Checking").Sum(a => a.Amount);
            var MAX2 = acc.Where(a => a.Account == "Credit").Sum(a => a.Amount);


            Assert.AreEqual(150, MAX1);
            Assert.AreEqual(150, MAX2);

        }

        [TestMethod]  //List the number of transactions from each account between April 5th and 7th (Considered including 5th and 7th)
        public void NoofTransBetwDate()
        {
            var ob = new CheckBookVM();
            ob.Fill();

            DateTime firstDate = new DateTime(2015, 05, 04);
            DateTime secondDate = new DateTime(2015, 07, 04);

            var numDate = ob.Transactions.Where(t => t.Date >= firstDate).Where(t => t.Date <= secondDate);
            var count1 = numDate.Where(t => t.Account == "Checking").Count();
            var count2 = numDate.Where(t => t.Account == "Credit").Count();

            Assert.AreEqual(3, count1);
            Assert.AreEqual(3, count2);
        }
    }
}
