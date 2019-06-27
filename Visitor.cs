using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Antlr4.Runtime.Misc;
using FlyLang.Interpreter;
using FlyLang.Parser;

namespace FlyLang
{
    public class Visitor : FlyLangBaseVisitor<Node>
    {
        public ActionTree Tree;
        public Visitor()
        {
            Tree = new ActionTree();
        }
        public override Node VisitProgram(FlyLangParser.ProgramContext context)
        {
            foreach (var item in context.children)
            {
                Tree.Add(Visit(item));
            }
            return Tree;
        }
        public override Node VisitStatement([NotNull] FlyLangParser.StatementContext context)
        {
            if (context.assignment() != null)
                return Visit(context.assignment());
            if (context.definition() != null)
                return Visit(context.definition());
            if (context.methodCall() != null)
                return Visit(context.methodCall());
            if (context.use() != null)
                return Visit(context.use());
            if (context.@if() != null)
                return Visit(context.@if());
            if (context.@while() != null)
                return Visit(context.@while());
            if (context.@return() != null)
                return Visit(context.@return());
            return base.VisitStatement(context);
        }
        public override Node VisitExpression([NotNull] FlyLangParser.ExpressionContext context)
        {
            if (context.op != null)
            {
                var op = context.op;
                return new Expression(Visit(context.left), Visit(context.right), op.Text);
            }
            if (context.methodCall() != null)
                return Visit(context.methodCall());
            if (context.varCall() != null)
                return Visit(context.varCall());
            if (context.@int() != null || context.@string() != null || context.@float() != null)
                return base.VisitExpression(context);
            if (context.boolean() != null)
                return new Literal(context.boolean().GetText() == "true");
            if (context.PRL() != null && context.PRR() != null && context.expression() != null)
                return Visit(context.expression(0));
            throw new Exception();
        }
        public override Node VisitVarCall([NotNull] FlyLangParser.VarCallContext context)
        {
            return new VarCall(context.ID().GetText());
        }
        public override Node VisitDefinition([NotNull] FlyLangParser.DefinitionContext context)
        {
            var action = Visit(context.expression().action()) as ActionNode;
            action.ArgNames = context.names().ID().Select(x => x.GetText()).ToArray();
            return new Definition(context.ID().GetText(), action);
        }
        public override Node VisitAction([NotNull] FlyLangParser.ActionContext context)
        {
            var subnodes = new List<Node>();
            foreach (var n in context.statement())
                subnodes.Add(Visit(n));
            return new ActionNode(subnodes.ToArray());
        }
        public override Node VisitAssignment([NotNull] FlyLangParser.AssignmentContext context)
        {
            return new Assignment(context.ID().GetText(), Visit(context.expression()));
        }

        public override Node VisitMethodCall([NotNull] FlyLangParser.MethodCallContext context)
        {
            var args = new List<Node>();
            if (context.expression() != null)
                foreach (var expr in context.expression())
                {
                    args.Add(Visit(expr));
                }
            return new MethodCall(context.ID().GetText(), args.ToArray());
        }
        public override Node VisitUse([NotNull] FlyLangParser.UseContext context)
        {
            return new UseStatement(context.ID().GetText());
        }
        public override Node VisitWhile([NotNull] FlyLangParser.WhileContext context)
        {
            return new WhileStatement(Visit(context.expression()), Visit(context.action()));
        }
        public override Node VisitReturn([NotNull] FlyLangParser.ReturnContext context)
        {
            return new Return(Visit(context.expression()));
        }
        public override Node VisitIf([NotNull] FlyLangParser.IfContext context)
        {
            var statement = new IfStatement(Visit(context.expression()), Visit(context.action()));

            if(context.elif() != null)
            {
                var expr = new List<Node>();
                var actions = new List<Node>();
                var elifs = context.elif();
                foreach(var e in elifs)
                {
                    expr.Add(Visit(e.expression()));
                    actions.Add(Visit(e.action()));
                }
                statement.ElifActions = actions.ToArray();
                statement.ElifExpressions = expr.ToArray();
            }
            if(context.@else() != null)
            {
                statement.ElseAction = Visit(context.@else().action());
            }

            return statement;
        }

        // Handling Literals: 
        public override Node VisitFloat([NotNull] FlyLangParser.FloatContext context)
        {
            return Literal.From(float.Parse(context.GetText(), CultureInfo.InvariantCulture));
        }
        public override Node VisitInt([NotNull] FlyLangParser.IntContext context)
        {
            return Literal.From(int.Parse(context.GetText()));
        }
        public override Node VisitString([NotNull] FlyLangParser.StringContext context)
        {
            var text = context.GetText();
            text = text.Substring(1, text.Length - 2);
            return Literal.From(text);
        }
    }
}