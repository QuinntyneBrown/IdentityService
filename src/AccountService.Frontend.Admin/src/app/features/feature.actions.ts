import { Feature } from "./feature.model";

export const featureActions = {
    ADD: "[Feature] Add",
    EDIT: "[Feature] Edit",
    DELETE: "[Feature] Delete",
    FEATURES_CHANGED: "[Feature] Features Changed"
};

export class FeatureEvent extends CustomEvent {
    constructor(eventName:string, feature: Feature) {
        super(eventName, {
            bubbles: true,
            cancelable: true,
            detail: { feature }
        });
    }
}

export class FeatureAdd extends FeatureEvent {
    constructor(feature: Feature) {
        super(featureActions.ADD, feature);        
    }
}

export class FeatureEdit extends FeatureEvent {
    constructor(feature: Feature) {
        super(featureActions.EDIT, feature);
    }
}

export class FeatureDelete extends FeatureEvent {
    constructor(feature: Feature) {
        super(featureActions.DELETE, feature);
    }
}

export class FeaturesChanged extends CustomEvent {
    constructor(features: Array<Feature>) {
        super(featureActions.FEATURES_CHANGED, {
            bubbles: true,
            cancelable: true,
            detail: { features }
        });
    }
}
