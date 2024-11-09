
int []FavNumber=new int[]{1,2,3,4,5,6,7,8,9};
Random rand=new Random(FavNumber[0]);
for (int i = 0; i <FavNumber.Length ; i++)
{
  var result=rand.Next(FavNumber[i]);
  //System.Console.WriteLine(rand.Next(FavNumber[i]));
  System.Console.Write("Random Number from Array:"+result);
  
}

