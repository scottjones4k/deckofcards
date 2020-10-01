import _ from 'lodash';

class ApiService {

    async fetch(endpoint: string, properties?: any) {
        return fetch(endpoint, properties);
    }

    async post(endpoint: string, data?: any, properties?: any) {
        const postData = {
            'method': 'POST', 'body': JSON.stringify(data || {}), headers: { 'Content-Type': 'application/json' }
        };
        let props = _.defaultsDeep(postData, properties || {});
        return await this.fetch(endpoint, props);
    }
}

const apiService = new ApiService();

export default apiService;