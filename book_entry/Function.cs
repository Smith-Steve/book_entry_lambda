using Amazon.Lambda.Core;
/**
 * Copyright 2010-2019 Amazon.com, Inc. or its affiliates. All Rights Reserved.
 *
 * This file is licensed under the Apache License, Version 2.0 (the "License").
 * You may not use this file except in compliance with the License. A copy of
 * the License is located at
 *
 * http://aws.amazon.com/apache2.0/
 *
 * This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
 * CONDITIONS OF ANY KIND, either express or implied. See the License for the
 * specific language governing permissions and limitations under the License.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;

namespace com.amazonaws.codesamples
{
	class MidlevelItemCRUD
	{
		private static AmazonDynamoDBClient client = new AmazonDynamoDBClient();
		private static string tableName = "ProductCatalog";
		// The sample uses the following id PK value to add book item.
		private static int sampleBookId = 555;

		static void Main(string[] args)
		{
			try
			{
				Table productCatalog = Table.LoadTable(client, tableName);
				CreateBookItem(productCatalog);
				// Couple of sample updates.
			}
			catch (AmazonDynamoDBException e) { Console.WriteLine(e.Message); }
			catch (AmazonServiceException e) { Console.WriteLine(e.Message); }
			catch (Exception e) { Console.WriteLine(e.Message); }
		}

		// Creates a sample book item.
		private static void CreateBookItem(Table productCatalog)
		{
			Console.WriteLine("\n*** Executing CreateBookItem() ***");
			var book = new Document();
			book["Id"] = sampleBookId;
			book["Title"] = "Book " + sampleBookId;
			book["Price"] = 19.99;
			book["ISBN"] = "111-1111111111";
			book["Authors"] = new List<string> { "Author 1", "Author 2", "Author 3" };
			book["PageCount"] = 500;
			book["Dimensions"] = "8.5x11x.5";
			book["InPublication"] = new DynamoDBBool(true);
			book["InStock"] = new DynamoDBBool(false);
			book["QuantityOnHand"] = 0;

			productCatalog.PutItemAsync(book);
		}
	}
}
