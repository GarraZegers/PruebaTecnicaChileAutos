export class EpisodeFilter {

    constructor(
        public name: string = '',
        public episode: string = ''
    ){}

    toQueryString(): string {
        const params: string [] = [];

        if(!!this.name.trim()) params.push(`name=${encodeURIComponent(this.name)}`)
        if(!!this.episode.trim()) params.push(`episode=${encodeURIComponent(this.episode)}`)

        return params.join('&');
    }

    hasFilters() : boolean {
        return !!this.name || !!this.episode;
    }
}