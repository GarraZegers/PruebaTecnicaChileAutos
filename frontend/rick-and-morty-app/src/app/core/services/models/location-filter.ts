export class LocationFilter {
  name?: string;
  type?: string;
  dimension?: string = "";

  toQueryString(): string {
    const params: string[] = [];

    if (!!this.name) params.push(`name=${encodeURIComponent(this.name)}`);
    if (!!this.type) params.push(`type=${encodeURIComponent(this.type)}`);
    if (!!this.dimension) params.push(`dimension=${encodeURIComponent(this.dimension)}`);

    return params.join('&');
  }

   hasFilters() : boolean {
        return !!this.name || !!this.type || !!this.dimension;
    }
}