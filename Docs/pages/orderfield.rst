OrderField
==========

An object that holds a field for ordering purposes.

Creating a new Instance
-----------------------

Constructor accepts 2 parameters, a Name and an Order type.

.. highlight:: c#

By literal string:

::

	// Ascending
	var orderField = new OrderField("Id", Order.Ascending);
	
	// Descending
	var orderField = new OrderField("Id", Order.Descending);

By class property:

::

	// Ascending
	var orderField = new OrderField(nameof(Customer.Id), Order.Ascending);
	
	// Descending
	var orderField = new OrderField(nameof(Customer.Id), Order.Descending);

AsEnumerable
------------

Converts an instance of an `OrderField` into an `IEnumerable<OrderField>` object.

.. highlight:: c#

::

	// Initialize an order field
	var orderField = new OrderField(nameof(Customer.Id), Order.Ascending);

	// Convert to enumerable
	var orderFields = orderField.AsEnumerable();

Ascending
---------

Parses a property from the data entity object based on the given `Expression` and converts the result to `OrderField` object with `Order.Ascending` value.

.. highlight:: c#

::

	var orderField = OrderField.Ascending<Customer>(c => c.Id);

Descending
----------

Parses a property from the data entity object based on the given `Expression` and converts the result to `OrderField` object with `Order.Descending` value.

.. highlight:: c#

::

	var orderField = OrderField.Descending<Customer>(c => c.Id);

Parse (via Expression)
----------------------

Parses a property from the data entity object based on the given `Expression` and converts the result to `OrderField` object.

.. highlight:: c#

Parse ascending:

::

	var orderField = OrderField.Parse<Customer>(c => c.Id, Order.Ascending);

Parse descending:

::

	var orderField = OrderField.Parse<Customer>(c => c.Id, Order.Descending);

Parse (via Object)
----------------------

Parses an object properties to be used for ordering. The object can have multiple properties for ordering and each property must have a value of `Enumerations.Order` enumeration.

.. highlight:: c#

::

	var orderFields = OrderField.Parse(new
	{
		UpdateDate = Order.Descending,
		FirstName = Order.Ascending
	});

Usage of OrderField
-------------------

The order field object is useful on certain operations.

Being the order fields in `BatchQuery` Operation:

::

	using (var connection = new SqlConnection>(@"Server=.;Database=Northwind;Integrated Security=SSPI;").EnsureOpen())
	{
		var orderBy = new
		{
			UpdatedDate = Order.Descending,
			FirstName = Order.Ascending
		};
		var customers = connection.BatchQuery<Customer>(page: 0,
			rowsPerBatch: 100,
			orderBy: OrderField.Parse(orderBy));
	}

Being the order fields in `Query` operation:

::

	using (var connection = new SqlConnection>(@"Server=.;Database=Northwind;Integrated Security=SSPI;").EnsureOpen())
	{
		var customerOrders = connection.Query<Order>(o => o.CustomerId == 1,
			orderby: OrderField.Parse(new { Id = Order.Ascending }));
	}
