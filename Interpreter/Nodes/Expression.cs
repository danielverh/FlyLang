using System;
using FlyLang.Parser;

namespace FlyLang.Interpreter.Nodes
{
    public class Expression : Node
    {
        public Expression(Node left, Node right, string op)
        {
            Nodes.Add(left);
            Nodes.Add(right);
            Operator = op;
        }
        public Expression(Node[] nodes)
        {
            AddRange(nodes);
        }
        public string Operator { get; }
        public Node Left => Nodes[0];
        public Node Right => Nodes[1];
        public override dynamic Invoke()
        {
            if (Operator != null)
            {
                return UseOperator();
            }
            if (HasChildren && Nodes.Count == 1)
                return Nodes[0].Invoke();
            return null;
        }
        private dynamic UseOperator()
        {
            var leftValue = Left.Invoke(Parent);
            var rightValue = Right.Invoke(Parent);
            if (Helper.IsNumeric(leftValue, rightValue))
            {
                switch (Operator)
                {
                    case "+":
                        return leftValue + rightValue;
                    case "*":
                        return leftValue * rightValue;
                    case "-":
                        return leftValue - rightValue;
                    case "/":
                        return leftValue / rightValue;
                    case "==":
                        return leftValue == rightValue;
                    case "<=":
                        return leftValue <= rightValue;
                    case ">=":
                        return leftValue >= rightValue;
                    case "!=":
                        return leftValue != rightValue;
                    case ">":
                        return leftValue > rightValue;
                    case "<":
                        return leftValue < rightValue;
                    default:
                        throw new Exception();
                }
            }
            else if (leftValue is string && rightValue is string)
            {
                if (Operator == "+")
                    return leftValue + rightValue;
                else if (Operator == "==")
                    return leftValue == rightValue;
            }
            throw new Exception();
        }
    }
}