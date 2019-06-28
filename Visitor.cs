using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Antlr4.Runtime.Misc;
using FlyLang.Interpreter;
using FlyLang.Interpreter.Nodes;
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
            if (context.classDef() != null)
                return Visit(context.classDef());
            if (context.methodCall() != null)
                return Visit(context.methodCall());
            if (context.use() != null)
                return Visit(context.use());
            if (context.ifStmt() != null)
                return Visit(context.ifStmt());
            if (context.whileStmt() != null)
                return Visit(context.whileStmt());
            if (context.returnStmt() != null)
                return Visit(context.returnStmt());
            return base.VisitStatement(context);
        }
        public override Node VisitClassDef([NotNull] FlyLangParser.ClassDefContext context)
        {
            var assignments = new List<Node>();
            var defs = new List<Node>();
            foreach (var item in context.assignment())
                assignments.Add(Visit(item));
            foreach (var item in context.definition())
                defs.Add(Visit(item));
            return new ClassDefNode(context.ID().GetText(), defs.ToArray(), assignments.ToArray());
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
            if (context.intLit() != null || context.@string() != null || context.floatLit() != null)
                return base.VisitExpression(context);
            if (context.boolean() != null)
                return new Literal(context.boolean().GetText() == "true");
            if (context.PRL() != null && context.PRR() != null && context.expression() != null)
                return Visit(context.expression(0));
            if (context.array() != null)
                return Visit(context.array());
            if (context.dictionary() != null)
                return Visit(context.dictionary());
            throw new Exception();
        }
        public override Node VisitVarCall([NotNull] FlyLangParser.VarCallContext context)
        {
            if (context.SBL() != null)
                return new ArrayVarCall(context.ID().GetText(), Visit(context.expression()));
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
            if(context.SBL() != null)
            {
                return new ArrayAssignment(context.ID().GetText(), 
                    Visit(context.expression(0)), Visit(context.expression(1)));
            }
            if (context.ADD().Length == 2)
                return new Assignment(context.ID().GetText(),
                    new Expression(new VarCall(context.ID().GetText()),
                    new Literal(1), "+"));
            if (context.SUB().Length == 2)
                return new Assignment(context.ID().GetText(),
                    new Expression(new VarCall(context.ID().GetText()),
                    new Literal(1), "-"));
            return new Assignment(context.ID().GetText(), Visit(context.expression(0)));
        }

        public override Node VisitMethodCall([NotNull] FlyLangParser.MethodCallContext context)
        {
            var args = new List<Node>();
            if (context.expression() != null)
                foreach (var expr in context.expression())
                {
                    args.Add(Visit(expr));
                }
            if(context.id().ID().Length > 1)
            {
                var ids = context.id().ID();
                Node target = null;
                if (ids.Length == 2)
                    target = new VarCall(ids[0].GetText());
                return new MethodCall(ids.Last().GetText(), args.ToArray(), target);
            }
            return new MethodCall(context.id().GetText(), args.ToArray());
        }
        public override Node VisitUse([NotNull] FlyLangParser.UseContext context)
        {
            var use = new UseStatement(context.id().GetText());
            ActionTree.UseStatements.Add(use);
            return use;
        }
        public override Node VisitWhileStmt([NotNull] FlyLangParser.WhileStmtContext context)
        {
            return new WhileStatement(Visit(context.expression()), Visit(context.action()));
        }
        public override Node VisitReturnStmt([NotNull] FlyLangParser.ReturnStmtContext context)
        {
            return new Return(Visit(context.expression()));
        }
        public override Node VisitIfStmt([NotNull] FlyLangParser.IfStmtContext context)
        {
            var statement = new IfStatement(Visit(context.expression()), Visit(context.action()));

            if (context.elifStmt() != null)
            {
                var expr = new List<Node>();
                var actions = new List<Node>();
                var elifs = context.elifStmt();
                foreach (var e in elifs)
                {
                    expr.Add(Visit(e.expression()));
                    actions.Add(Visit(e.action()));
                }
                statement.ElifActions = actions.ToArray();
                statement.ElifExpressions = expr.ToArray();
            }
            if (context.elseStmt() != null)
            {
                statement.ElseAction = Visit(context.elseStmt().action());
            }

            return statement;
        }

        // Handling Literals: 
        public override Node VisitFloatLit([NotNull] FlyLangParser.FloatLitContext context)
        {
            return Literal.From(float.Parse(context.GetText(), CultureInfo.InvariantCulture));
        }
        public override Node VisitIntLit([NotNull] FlyLangParser.IntLitContext context)
        {
            return Literal.From(int.Parse(context.GetText()));
        }
        public override Node VisitString([NotNull] FlyLangParser.StringContext context)
        {
            var text = context.GetText();
            text = text.Substring(1, text.Length - 2);
            return Literal.From(text);
        }
        public override Node VisitArray([NotNull] FlyLangParser.ArrayContext context)
        {
            var items = new Node[context.expression().Length];
            for (int i = 0; i < items.Length; i++)
            {
                items[i] = Visit(context.expression(i));
            }
            return new ArrayNode(items);
        }

        public override Node VisitDictionary([NotNull] FlyLangParser.DictionaryContext context)
        {
            var items = new (Node, Node)[context.keyItem().Length];
            for (int i = 0; i < items.Length; i++)
            {
                var key = context.keyItem(i);
                items[i] = (Visit(key.key), Visit(key.value));
            }
            return new DictionaryNode(items);
        }
    }
}