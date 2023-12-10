import { AppState } from '../AppState'
import { Account } from '../models/Account.js'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { api } from './AxiosService'

class AccountService {
  async getAccount() {
    try {
      const res = await api.get('/account')
      AppState.account = new Account(res.data)
    } catch (err) {
      logger.error('HAVE YOU STARTED YOUR SERVER YET???', err)
    }
  }


  async editAccount(editable) {
    try {
      const res = await api.put('/account', editable)
      AppState.account = new Account(res.data)
    } catch (error) {
      Pop.error(error)
    }
  }
}

export const accountService = new AccountService()
