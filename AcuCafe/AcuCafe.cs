using System;

namespace AcuCafe
{
	public class AcuCafe
	{
		public static Barista Barista { get; } = new Barista();
		public static Drink OrderDrink(string type, bool hasMilk, bool hasSugar, bool hasChocolate)
		{
			Drink drink = new Drink();
			if (type == "Espresso")
			{
				drink = new Espresso();
			}
			else if (type == "HotTea")
				drink = new Tea();
			else if (type == "IceTea")
				drink = new IceTea();

			try
			{
				drink.HasMilk = hasMilk;
				drink.HasSugar = hasSugar;
				drink.Prepare(type);
			}
			catch (Exception ex)
			{
				Console.WriteLine("We are unable to prepare your drink.");
				System.IO.File.WriteAllText(@"Error.txt", ex.ToString());
			}

			return drink;
		}
	}

	public class Barista
	{
		public bool IsNotified { get; set; }
	}

	public class Drink
	{
		public const double MilkCost = 0.5;
		public const double SugarCost = 0.5;

		public bool HasMilk { get; set; }
		public bool HasSugar { get; set; }
		public bool HasChocolate { get; set; }
		public string Description { get; }
		public bool? IsPrepared { get; set; }

		public double Cost()
		{
			return 0;
		}

		public virtual void Prepare(string drink)
		{
			string message = "We are preparing the following drink for you: " + Description;
			if (HasMilk)
				message += " with milk";
			else
				message += " without milk";

			if (HasSugar)
				message += " with sugar";
			else
				message += " without sugar";

			Console.WriteLine(message);
		}
	}

	public class Espresso : Drink
	{
		public new string Description
		{
			get { return "Espresso"; }
		}

		public new double Cost()
		{
			double cost = 1.8;

			if (HasMilk)
				cost += MilkCost;

			if (HasSugar)
				cost += SugarCost;

			return cost;
		}

		public override void Prepare(string drink)
		{
			string message = "We are preparing the following drink for you: " + Description;
			message += HasMilk ? " with milk" : " without milk";
			message += HasSugar ? " with sugar" : " without sugar";
			message += HasChocolate ? " with chocolate" : " without chocolate";
			Console.WriteLine(message);
		}
	}

	public class Tea : Drink
	{
		public new string Description
		{
			get { return "Hot tea"; }
		}

		public new double Cost()
		{
			double cost = 1;

			if (HasMilk)
				cost += MilkCost;

			if (HasSugar)
				cost += SugarCost;

			return cost;
		}

		public override void Prepare(string drink)
		{
			string message = "We are preparing the following drink for you: " + Description;
			message += HasMilk ? " with milk" : " without milk";
			message += HasSugar ? " with sugar" : " without sugar";
			if (HasChocolate) throw new ArgumentException("This drink cannot contain chocolate", nameof(HasChocolate));
			Console.WriteLine(message);
		}
	}

	public class IceTea : Drink
	{
		public new string Description
		{
			get { return "Ice tea"; }
		}

		public new double Cost()
		{
			double cost = 1.5;

			if (HasMilk)
				cost += MilkCost;

			if (HasSugar)
				cost += SugarCost;

			return cost;
		}


		public override void Prepare(string drink)
		{
			string message = "We are preparing the following drink for you: " + Description;
			if (HasMilk) throw new ArgumentException("This drink cannot contain milk", nameof(HasChocolate));
			message += " without milk";
			message += HasSugar ? " with sugar" : " without sugar";
			if (HasChocolate) throw new ArgumentException("This drink cannot contain chocolate", nameof(HasChocolate));
			Console.WriteLine(message);
		}
	}
}