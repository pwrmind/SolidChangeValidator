using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Ideas;
    public class SolidViolationDetector : CSharpSyntaxWalker
    {
        private readonly List<SolidPrinciple> _violatedPrinciples;

        public SolidViolationDetector()
        {
            _violatedPrinciples = new List<SolidPrinciple>();
        }

        public List<SolidPrinciple> DetectViolations(string codePath)
        {
            _violatedPrinciples.Clear();

            if (!File.Exists(codePath))
            {
                throw new FileNotFoundException($"File not found: {codePath}");
            }

            var sourceText = File.ReadAllText(codePath);
            var syntaxTree = CSharpSyntaxTree.ParseText(sourceText);
            var root = syntaxTree.GetRoot();

            Visit(root);

            return _violatedPrinciples;
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            // Проверка принципа единственной ответственности (SRP)
            if (node.Members.Count > 1)
            {
                _violatedPrinciples.Add(SolidPrinciple.SRP);
            }

            base.VisitClassDeclaration(node);
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            // Проверка принципа открытости/закрытости (OCP)
            if (node.Modifiers.Any(m => m.IsKind(SyntaxKind.VirtualKeyword) ||
                                       m.IsKind(SyntaxKind.OverrideKeyword)))
            {
                _violatedPrinciples.Add(SolidPrinciple.OCP);
            }

            base.VisitMethodDeclaration(node);
        }

        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            // Проверка принципа разделения интерфейсов (ISP)
            if (node.Members.Count > 5)
            {
                _violatedPrinciples.Add(SolidPrinciple.ISP);
            }

            base.VisitInterfaceDeclaration(node);
        }

        public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            // Проверка принципа инверсии зависимостей (DIP)
            if (node.ParameterList.Parameters.Count > 3)
            {
                _violatedPrinciples.Add(SolidPrinciple.DIP);
            }

            base.VisitConstructorDeclaration(node);
        }

        // Принцип подстановки Барбары Лисков (LSP) не реализован, так как это требует более сложного анализа
    }
