﻿namespace Mages.Core.Tests
{
    using Mages.Core.Ast;
    using Mages.Core.Ast.Expressions;
    using NUnit.Framework;

    [TestFixture]
    public class ObjectExpressionTests
    {
        [Test]
        public void ObjectLiteralWithSingleKeyValue()
        {
            var source = "{ key: value }";
            var tokens = source.ToTokenStream();
            var parser = new ExpressionParser();
            var result = parser.ParseExpression(tokens);

            Assert.IsInstanceOf<ObjectExpression>(result);
            var obj = (ObjectExpression)result;
            Assert.AreEqual(1, obj.Values.Length);

            var property = obj.Values[0] as PropertyExpression;

            Assert.IsNotNull(property);
            Assert.IsInstanceOf<IdentifierExpression>(property.Name);
            Assert.IsInstanceOf<VariableExpression>(property.Value);

            var name = (IdentifierExpression)property.Name;
            var value = (VariableExpression)property.Value;

            Assert.AreEqual("key", name.Name);
            Assert.AreEqual("value", value.Name);
        }

        [Test]
        public void ObjectLiteralWithMultipleKeyValuesMixedExpressions()
        {
            var source = "{ one: 1, two: false, three: 2 * 3 }";
            var tokens = source.ToTokenStream();
            var parser = new ExpressionParser();
            var result = parser.ParseExpression(tokens);

            Assert.IsInstanceOf<ObjectExpression>(result);
            var obj = (ObjectExpression)result;
            Assert.AreEqual(3, obj.Values.Length);

            var property1 = obj.Values[0] as PropertyExpression;
            var property2 = obj.Values[1] as PropertyExpression;
            var property3 = obj.Values[2] as PropertyExpression;

            foreach (var property in new[] { property1, property2, property3 })
            {
                Assert.IsNotNull(property);
                Assert.IsInstanceOf<IdentifierExpression>(property.Name);
            }

            var name1 = (IdentifierExpression)property1.Name;
            var value1 = (ConstantExpression)property1.Value;

            Assert.AreEqual("one", name1.Name);
            Assert.AreEqual(1.0, value1.Value);

            var name2 = (IdentifierExpression)property2.Name;
            var value2 = (ConstantExpression)property2.Value;

            Assert.AreEqual("two", name2.Name);
            Assert.AreEqual(false, value2.Value);

            var name3 = (IdentifierExpression)property3.Name;
            var value3 = (BinaryExpression.Multiply)property3.Value;

            Assert.AreEqual("three", name3.Name);
            Assert.AreEqual(2.0, ((ConstantExpression)value3.LValue).Value);
            Assert.AreEqual(3.0, ((ConstantExpression)value3.RValue).Value);
        }

        [Test]
        public void TightEmptyObjectLiteral()
        {
            var source = "{}";
            var tokens = source.ToTokenStream();
            var parser = new ExpressionParser();
            var result = parser.ParseExpression(tokens);

            Assert.IsInstanceOf<ObjectExpression>(result);
            var obj = (ObjectExpression)result;
            Assert.AreEqual(0, obj.Values.Length);
            Assert.AreEqual(1, obj.Start.Column);
            Assert.AreEqual(2, obj.End.Column);
        }

        [Test]
        public void RelaxedEmptyObjectLiteral()
        {
            var source = " {  } ";
            var tokens = source.ToTokenStream();
            var parser = new ExpressionParser();
            var result = parser.ParseExpression(tokens);

            Assert.IsInstanceOf<ObjectExpression>(result);
            var obj = (ObjectExpression)result;
            Assert.AreEqual(0, obj.Values.Length);
            Assert.AreEqual(2, obj.Start.Column);
            Assert.AreEqual(5, obj.End.Column);
        }

        [Test]
        public void ObjectLiteralWithSingleKeyValueTrailingComma()
        {
            var source = "{ key: value , }";
            var tokens = source.ToTokenStream();
            var parser = new ExpressionParser();
            var result = parser.ParseExpression(tokens);

            Assert.IsInstanceOf<ObjectExpression>(result);
            var obj = (ObjectExpression)result;
            Assert.AreEqual(1, obj.Values.Length);

            var property = obj.Values[0] as PropertyExpression;

            Assert.IsNotNull(property);
            Assert.IsInstanceOf<IdentifierExpression>(property.Name);
            Assert.IsInstanceOf<VariableExpression>(property.Value);

            var name = (IdentifierExpression)property.Name;
            var value = (VariableExpression)property.Value;

            Assert.AreEqual("key", name.Name);
            Assert.AreEqual("value", value.Name);
        }
    }
}