﻿namespace FluentValidation.Mvc {
	using System.Collections.Generic;
	using System.Web.Mvc;
	using Internal;
	using Validators;

	internal class RequiredFluentValidationPropertyValidator : FluentValidationPropertyValidator {
		public RequiredFluentValidationPropertyValidator(ModelMetadata metadata, ControllerContext controllerContext, PropertyRule rule, IPropertyValidator validator) : base(metadata, controllerContext, rule, validator) {
			bool isNonNullableValueType = !TypeAllowsNullValue(metadata.ModelType);
			bool nullWasSpecified = metadata.Model == null;

			ShouldValidate = isNonNullableValueType && nullWasSpecified;
		}

		public override IEnumerable<ModelClientValidationRule> GetClientValidationRules() {
			var formatter = new MessageFormatter().AppendPropertyName(Rule.PropertyDescription);
			var message = formatter.BuildMessage(Validator.ErrorMessageSource.GetString());
			yield return new ModelClientValidationRequiredRule(message);
		}

		public override bool IsRequired {
			get { return true; }
		}
	}
}