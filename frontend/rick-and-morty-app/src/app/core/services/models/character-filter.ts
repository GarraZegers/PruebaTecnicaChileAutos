export class CharacterFilter {
  name?: string;
  status?: string;
  species?: string;
  type?: string;
  gender?: string;

  toQueryString(): string {
    const params: string[] = [];

    if (!!this.name) params.push(`name=${encodeURIComponent(this.name)}`);
    if (!!this.status) params.push(`status=${encodeURIComponent(this.status)}`);
    if (!!this.species) params.push(`species=${encodeURIComponent(this.species)}`);
    if (!!this.type) params.push(`type=${encodeURIComponent(this.type)}`);
    if (!!this.gender) params.push(`gender=${encodeURIComponent(this.gender)}`);

    return params.join('&');
  }
}