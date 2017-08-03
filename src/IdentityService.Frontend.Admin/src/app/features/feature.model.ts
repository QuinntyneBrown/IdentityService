export class Feature { 
    public id:any;

    public name: string;

    public url: string;

    public fromJSON(data: any): Feature {
        let feature = new Feature();
        feature.name = data.name;
        feature.url = data.url;
        return feature;
    }
}
