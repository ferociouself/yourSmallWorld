using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tuple<T1, T2>  {
	
	public T1 item1;
	public T2 item2;

	public Tuple(T1 one, T2 two){
		this.item1 = one;
		this.item2 = two;
	}
}
