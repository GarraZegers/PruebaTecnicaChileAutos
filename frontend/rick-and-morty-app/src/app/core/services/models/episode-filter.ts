export class EpisodeFilter {

    constructor(
        public name: string = '',
        public episode: string = ''
    ){}

    toQueryParams(): {[key:string]: string}{
        const params: {[key:string]: string} = {};

        if(this.name.trim()){
            params['name'] = this.name.trim();
        }

        if(this.episode.trim()){
            params['episode'] = this.episode.trim();
        }

        return params;
    }

    hasFilters() : boolean {
        return !!this.name || !!this.episode;
    }
}