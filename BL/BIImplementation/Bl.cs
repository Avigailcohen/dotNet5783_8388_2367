﻿using BIApi;

namespace BIImplementation;
public class Bl : IBl
{
    public Bl() { }

    public IOrder Order { get; set; } = new Order();
    public IProduct Product { get; set; } = new Product();
    public ICart Cart { get; set; } = new Cart();


}
