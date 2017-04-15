export class Feature { 
    public id:any;
    public name:string;

    public fromJSON(data: { name:string }): Feature {
        let feature = new Feature();
        feature.name = data.name;
        return feature;
    }
}
